﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Taxi_Database.Models;

namespace Taxi_Database.Repository
{
    public interface IRepository
    {
        Task SaveClient(Client client);
        bool FindClient(string password);
        Task EditClient(Client client);
        IEnumerable<Client> GetClients();
        Client GetClient(int id);
        Task DeleteClient(int id);
        int SaveOrder(Order order);
        IEnumerable<Order> GetRequests();
        IEnumerable<Order> GetRequestsByClientId(int id);
        Task DeleteRequest(int id);
        IEnumerable<ReadyOrders> GetOrders();
        List<ReadyOrders> GetOrdersByClientId(int id);
        Task DeleteOrder(int id);
        Task SavePassengers(int orderId, int firstId, int secondId, int thirdId, int forthId);
        Task SaveLocation(Location location);
        IEnumerable<Location> GetLocations();
        IEnumerable<HistoryOfLocation> GetHistoryOfLocations();
        int GetCountOfRates(int id);
        double GetRating(int id);
        Task UpdateRating(int id, int countOfRates, double rating);
    }

    public interface IRating
    {
        Task Create(int whoId, int whomId, int orderId, int rating);
    }
}
