using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
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

        public bool FindClient(string password)
        {
            var client = db.Client.Where(x => x.Password == password)
                .FirstOrDefault();
            if (client == null)
                return false;
            return true;
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

        public Client GetClient(int id)
        {
            var client = db.Client.Find(id);
            return client;
        }

        public async Task DeleteClient(int id)
        {
            var client = db.Client.Find(id);
            db.Client.Remove(client);
            await db.SaveChangesAsync();
        }

        public async Task SaveOrder(Order order)
        {
            db.Order.Add(order);
            db.HistoryOfLocation.Find(order.StartPointId).CountOfDepartures++;
            db.HistoryOfLocation.Find(order.FinishPointId).CountOfArrivals++;
            await db.SaveChangesAsync();
        }

        public IEnumerable<Order> GetOrders()
        {
            var orders = db.Order.Select(x => x);
            return orders;
        }

        public IEnumerable<Order> GetOrdersByClientId (int id)
        {
            var orders = db.Order.Where(x => x.UserId == id);
            return orders;
        }

        public async Task DeleteOrder(int id)
        {
            var order = db.Order.Find(id);
            db.Order.Remove(order);
            //db.HistoryOfOrder.Add(new HistoryOfOrder(order.StartPointId, order.FinishPointId, 
            //    order.DepartureTime, order.OrderTime,  ));
            await db.SaveChangesAsync();
        }

        public IEnumerable<HistoryOfOrder> GetHistoryOfOrders()
        {
            var orders = db.HistoryOfOrder.Select(x => x);
            return orders;
        }

        public async Task SaveLocation(Location location)
        {
            db.Location.Add(location);
            await db.SaveChangesAsync();
            var newLocation = db.Location.Last();
            db.HistoryOfLocation.Add(new HistoryOfLocation(newLocation.Id));
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

        public int GetCountOfRates(int id)
        {
            var client = db.Client.Find(id);
            return client.CountOfRates;
        }

        public double GetRating(int id)
        {
            var client = db.Client.Find(id);
            return client.Rating;
        }

        public async Task UpdateRating(int id, int countOfRates, double rating)
        {
            var client = db.Client.Find(id);
            client.CountOfRates = countOfRates;
            client.Rating = rating;
            db.Entry(client).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }
    }
}
