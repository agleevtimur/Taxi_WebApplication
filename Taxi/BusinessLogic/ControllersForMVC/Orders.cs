using BusinessLogic.Algorithms;
using BusinessLogic.ModelsForControllers;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taxi_Database.Context;
using Taxi_Database.Models;
using Taxi_Database.Repository;

namespace BusinessLogic.ControllersForMVC
{
    public class Orders : IOrderController
    {
        private readonly ApplicationContext context;

        public Orders(ApplicationContext context)
        {
            this.context = context;
        }

        public IndexOrderViewModel Index()
        {
            IRepository repository = new Repository(context);
            var orders = repository.GetRequests();
            var readyOrders = repository.GetOrders();
            return new IndexOrderViewModel { Orders = orders, ReadyOrders = readyOrders};
        }

        public CreateOrderViewModel CreateGet(string id)
        {
            ILocationController repository = new Locations(context);
            var locations = repository.Index();
            var list = new List<string>();
            foreach (var location in locations)
                list.Add(location.NameOfLocation);

            return new CreateOrderViewModel { Id = id, LocationsFrom = list, LocationsTo = list};
        }

        public async Task Create(string locationFrom, string locationTo, string time, int countOfPeople, string id)
        {
            IRepository repository = new Repository(context);
            Algorithm algorithm = new Algorithm(context);
            var info = await algorithm.Find(time, locationFrom, locationTo, countOfPeople, id);
            if (info != null)
                await repository.SavePassengers(info[0], info[1], info[2], info[3], info[4]);
        }

        public OrderWithClientViewModel GetOrder(int id)
        {
            IRepository repository = new Repository(context);
            var order = repository.GetOrder(id);
            var passengers = repository.GetPassengers(id);
            return new OrderWithClientViewModel { ReadyOrder = order, Clients = passengers };
        }

        public List<ReadyOrders> GetOrdersByClientId(string id)
        {
            IRepository repository = new Repository(context);
            var client = repository.GetClient(id);
            return repository.GetOrdersByClientId(client.Id);
        }

        public IEnumerable<Order> GetRequestsByClientId(string id)
        {
            IRepository repository = new Repository(context);
            var client = repository.GetClient(id);
            return repository.GetRequestsByClientId(client.Id);
        }

        public ReadyOrders GetReadyOrderId(int id)
        {
            IRepository repository = new Repository(context);
            return repository.GetReadyOrderId(id);
        }

        public async Task DeleteOrder(int id)
        {
            IRepository repository = new Repository(context);
            await repository.DeleteOrder(id);
        }

        public async Task Rating(string whoId, string whomId, int orderId, int newRating)
        {
            IRating rate = new RatingContext();
            await rate.Create(whoId, whomId, orderId, newRating);

            IRepository repository = new Repository(context);
            var countOfRates = repository.GetCountOfRates(whomId);
            var rating = repository.GetRating(whomId);

            INewRating newRate = new NewRating();
            var newCountOfRates = newRate.GetNewCountOfRates(countOfRates);
            var ratingOfClient = newRate.GetNewRating(countOfRates, rating, newRating);

            await repository.UpdateRating(whomId, newCountOfRates, ratingOfClient);
        }
    }
}
