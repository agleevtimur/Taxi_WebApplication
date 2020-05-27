using Microsoft.EntityFrameworkCore;
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

        public int CountOfClients()
        {
            return db.Client.Count();
        }

        public int CountOfClientsInMonth()
        {
            return db.Client.Where(x => x.RegisterTime.Month == DateTime.Now.Month).Count();
        }

        public int CountOfSubscription()
        {
            return db.Client.Where(x => x.Priority > 0).Count();
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

        public async Task<Client> GetClient(string id)
        {
            var client = await db.Client.Where(x => x.StringId == id)
                .FirstOrDefaultAsync();
            return client;
        }

        public Client GetClientByLogin(string login)
        {
            var client = db.Client.Where(x => x.ClientName == login)
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

        public async Task SaveOrder(ReadyOrders order)
        {
            db.ReadyOrders.Add(order);
            db.HistoryOfLocation.Find(order.StartPointId).CountOfDepartures++;
            db.HistoryOfLocation.Find(order.FinishPointId).CountOfArrivals++;
            await db.SaveChangesAsync();
        }

        public async Task<int> GetSaveOrderId(ReadyOrders order)
        {
            var neworder = await db.ReadyOrders.Where(x => x.OrderTime == order.OrderTime)
                .FirstOrDefaultAsync();
            return neworder.Id;
        }

        public IEnumerable<ReadyOrders> GetOrders()
        {
            var orders = db.ReadyOrders.Select(x => x);
            return orders;
        }

        public int CountOdReadyOrders()
        {
            return db.ReadyOrders.Count();
        }

        public int CountOfReadyOrdersInDay()
        {
            return db.ReadyOrders.Where(x => x.OrderTime.Day == DateTime.Now.Day).Count();
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
                lock (passenger)
                {
                    if (passenger.FirstId == id || passenger.SecondId == id
                        || passenger.ThirdId == id || passenger.ForthId == id)
                        ordersId.Add(passenger.OrderId);
                }
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
            var passengers = db.Passengers.Where(x => x.OrderId == id);
            var newpassengers = passengers.FirstOrDefault();
            list.Add(db.Client.Find(newpassengers.FirstId));
            list.Add(db.Client.Find(newpassengers.SecondId));
            list.Add(db.Client.Find(newpassengers.ThirdId));
            list.Add(db.Client.Find(newpassengers.ForthId));
            return list;
        }

        public async Task SavePassengers(int orderId, int firstId, int secondId, int thirdId, int forthId)
        {
            db.Passengers.Add(new Passengers(orderId, firstId, secondId, thirdId, forthId));
            await db.SaveChangesAsync();
        }

        public async Task SaveHistoryOfLocation(string location)
        {
            var newLocation = await db.Location.Where(x => x.NameOfLocation == location)
                .FirstOrDefaultAsync();
            db.HistoryOfLocation.Add(new HistoryOfLocation(newLocation.Id, newLocation.NameOfLocation));
            await db.SaveChangesAsync();
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
