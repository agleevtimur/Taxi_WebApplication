using System;
using System.Collections.Generic;
using System.Text;
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
        Task SaveOrder(Order order);
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrdersByClientId(int id);
        Task DeleteOrder(int id);
        IEnumerable<HistoryOfOrder> GetHistoryOfOrders();
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
