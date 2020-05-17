﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi_Database.Context;
using Taxi_Database.Models;

namespace Taxi_Database.Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationContext db;
        public Repository(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task SaveClient(Client client)
        {
            db.Client.Add(client);
            await db.SaveChangesAsync();
        }

        public async Task UpdateClient(string clientId)
        {
            var client = db.Client
               .Where(x => x.StringId == clientId)
               .FirstOrDefault();

            if (client.Priority != 0)//уменьшаем количество поездок в случае наличия приоритета
            {
                if (client.LeftOrdersPriority <= 1)//уменьшаем количество поездок
                    client.Priority = 0;//минимальный приоритет, т.е. без приоритета
                else
                    client.LeftOrdersPriority--;
            }

            client.CountOfTrips++;
            await db.SaveChangesAsync();
        }

        public async Task EditClient(Client client)
        {
            db.Client.Update(client);
            db.Entry(client).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public IEnumerable<Client> GetClients()
        {
            var clients = db.Client.Select(x => x);
            return clients;
        }

        public Client GetClient(string id)
        {
            var client = db.Client.Where(x => x.StringId == id)
                .FirstOrDefault();
            return client;
        }

        public Client GetClientForOrders(int id)
        {
            return db.Client.Find(id);
        }

        public async Task DeleteClient(string id)
        {
            var client = db.Client.Where(x => x.StringId == id)
                .FirstOrDefault();
            db.Client.Remove(client);
            await db.SaveChangesAsync();
        }

        public async Task SaveRequest(Order order)
        {
            db.Order.Add(order);
            await db.SaveChangesAsync();
        }

        public IEnumerable<Order> GetRequests()
        {
            var orders = db.Order.Select(x => x);
            return orders;
        }

        public IEnumerable<Order> GetRequestsByClientId (int id)
        {
            var orders = db.Order.Where(x => x.UserId == id);
            return orders;
        }

        public async Task DeleteRequest(int id)
        {
            var order = db.Order.Find(id);
            db.Order.Remove(order);
            await db.SaveChangesAsync();
        }

        public async Task<int> SaveOrder(Order order)
        {
            db.ReadyOrders.Add(new ReadyOrders
               (order.StartPointId, order.FinishPointId, order.DepartureTime, DateTime.Now));
            db.HistoryOfLocation.Find(order.StartPointId).CountOfDepartures++;
            db.HistoryOfLocation.Find(order.FinishPointId).CountOfArrivals++;
            await db.SaveChangesAsync();
            return db.ReadyOrders.Last().Id;
        }

        public IEnumerable<ReadyOrders> GetOrders()
        {
            var orders = db.ReadyOrders.Select(x => x);
            return orders;
        }

        public ReadyOrders GetOrder(int id)
        {
            return db.ReadyOrders.Find(id);
        }

        public List<ReadyOrders> GetOrdersByClientId (int id)
        {
            var readyOrders = new List<ReadyOrders>();
            var ordersId = GetOrdersId(id);
            foreach (var orderId in ordersId)
                readyOrders.Add(db.ReadyOrders.Find(orderId));
            return readyOrders;
        }

        private List<int> GetOrdersId(int id)
        {
            var ordersId = new List<int>();
            var passengers = db.Passengers;
            foreach (var passenger in passengers)
            {
                if (passenger.FirstId == id || passenger.SecondId == id
                    || passenger.ThirdId == id || passenger.ForthId == id)
                    ordersId.Add(passenger.OrderId);
            }

            return ordersId;
        }

        public ReadyOrders GetReadyOrderId(int id)
        {
            return db.ReadyOrders.Find(id);
        }

        public async Task DeleteOrder(int id)
        {
            var order = db.ReadyOrders.Find(id);
            db.ReadyOrders.Remove(order);
            await db.SaveChangesAsync();
        }

        public List<Client> GetPassengers(int id)
        {
            var list = new List<Client>();
            var passengers = db.Passengers.Where(x => x.OrderId == id)
                .FirstOrDefault();
            list.Add(db.Client.Find(passengers.FirstId));
            list.Add(db.Client.Find(passengers.SecondId));
            list.Add(db.Client.Find(passengers.ThirdId));
            list.Add(db.Client.Find(passengers.ForthId));
            return list;
        }

        public async Task SavePassengers(int orderId, int firstId, int secondId, int thirdId, int forthId)
        {
            db.Passengers.Add(new Passengers(orderId, firstId, secondId, thirdId, forthId));
            await db.SaveChangesAsync();
        }

        public async Task SaveLocation(Location location)
        {
            db.Location.Add(location);
            await db.SaveChangesAsync();
            var newLocation = db.Location.Last();
            db.HistoryOfLocation.Add(new HistoryOfLocation(newLocation.Id, newLocation.NameOfLocation));
            await db.SaveChangesAsync();
        }

        public IEnumerable<Location> GetLocations()
        {
            var locations = db.Location.Select(x => x);
            return locations;
        }

        public IEnumerable<HistoryOfLocation> GetHistoryOfLocations()
        {
            var locations = db.HistoryOfLocation.Select(x => x);
            return locations;
        }

        public int GetCountOfRates(string id)
        {
            var client = db.Client.Where(x => x.StringId == id)
                .FirstOrDefault();
            return client.CountOfRates;
        }

        public double GetRating(string id)
        {
            var client = db.Client.Where(x => x.StringId == id)
               .FirstOrDefault();
            return client.Rating;
        }

        public async Task UpdateRating(string id, int countOfRates, double rating)
        {
            var client = db.Client.Where(x => x.StringId == id)
               .FirstOrDefault();
            client.CountOfRates = countOfRates;
            client.Rating = rating;
            db.Entry(client).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
