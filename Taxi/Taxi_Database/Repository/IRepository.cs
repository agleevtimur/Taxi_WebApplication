using System.Collections.Generic;
using System.Threading.Tasks;
using Taxi_Database.Models;

namespace Taxi_Database.Repository
{
    public interface IRepository
    {
        Task SaveClient(Client client);
        int CountOfClients();
        int CountOfClientsInMonth();
        int CountOfSubscription();
        Task UpdateClient(string clientId);
        Task EditClient(Client client);
        IEnumerable<Client> GetClients();
        Task<Client> GetClient(string id);
        Client GetClientByLogin(string login);
        Client GetClientForOrders(int id);
        Task DeleteClient(string id);
        Task SaveRequest(Order order);
        int CountOdReadyOrders();
        int CountOfReadyOrdersInDay();
        IEnumerable<Order> GetRequests();
        IEnumerable<Order> GetRequestsByClientId(int id);
        Task DeleteRequest(int id);
        Task SaveOrder(ReadyOrders order);
        Task<int> GetSaveOrderId(ReadyOrders order);
        IEnumerable<ReadyOrders> GetOrders();
        ReadyOrders GetOrder(int id);
        List<ReadyOrders> GetOrdersByClientId(int id);
        ReadyOrders GetReadyOrderId(int id);
        Task DeleteOrder(int id);
        List<Client> GetPassengers(int id);
        Task SavePassengers(int orderId, int firstId, int secondId, int thirdId, int forthId);
        Task SaveLocation(Location location);
        Task<int> GetLocationId(string location);
        Task SaveHistoryOfLocation(string location);
        IEnumerable<Location> GetLocations();
        IEnumerable<HistoryOfLocation> GetHistoryOfLocations();
        int GetCountOfRates(string id);
        double GetRating(string id);
        Task UpdateRating(string id, int countOfRates, double rating);
    }

    public interface IRating
    {
        Task Create(string whoId, string whomId, int orderId, int rating);
    }
}
