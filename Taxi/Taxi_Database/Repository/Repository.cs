using Microsoft.EntityFrameworkCore;
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

        public int SaveOrder(Order order)
        {
            db.Order.Add(order);
            db.ReadyOrders.Add(new ReadyOrders
                (order.StartPointId, order.FinishPointId, order.DepartureTime, order.OrderTime));
            db.HistoryOfLocation.Find(order.StartPointId).CountOfDepartures++;
            db.HistoryOfLocation.Find(order.FinishPointId).CountOfArrivals++;
            db.SaveChangesAsync();
            return db.ReadyOrders.Last().Id;
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

        public IEnumerable<ReadyOrders> GetOrders()
        {
            var orders = db.ReadyOrders.Select(x => x);
            return orders;
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

        public async Task DeleteOrder(int id)
        {
            var order = db.ReadyOrders.Find(id);
            db.ReadyOrders.Remove(order);
            await db.SaveChangesAsync();
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
