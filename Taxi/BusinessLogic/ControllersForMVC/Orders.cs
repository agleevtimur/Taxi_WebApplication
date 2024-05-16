﻿using BusinessLogic.Algorithms;
using BusinessLogic.ModelsForControllers;
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
        private readonly LocationService locationService;
        public Orders(ApplicationContext context, LocationService locationService)
        {
            this.context = context;
            this.locationService = locationService;
        }
        
        public OrderIndexViewModel GetModel(string id)
        {
            return new OrderIndexViewModel { Id = id };
        }

        public IndexOrderViewModel Index(string id)
        {
            IRepository repository = new Repository(context);
            ILocationController repository1 = new Locations(locationService, context);
            var locations = repository1.Index();
            var orders = repository.GetRequests();
            var readyOrders = repository.GetOrders();
            return new IndexOrderViewModel { Id = id, Orders = orders, ReadyOrders = readyOrders, Locations = locations};
        }

        public CreateOrderViewModel CreateGet(string id)
        {
            ILocationController repository = new Locations(locationService, context);
            var locations = repository.Index();
            var list = new List<string>();
            foreach (var location in locations)
                list.Add(location.NameOfLocation);
            return new CreateOrderViewModel { Id = id, LocationsFrom = list, LocationsTo = list};
        }

        public IEnumerable<Location> Locations()
        {
            ILocationController repository = new Locations(locationService, context);
            var locations = repository.Index();
            return locations;
        }

        public async Task Create(string locationFrom, string locationTo, string time, int countOfPeople, string id)
        {
            IRepository repository = new Repository(context);
            Algorithm algorithm = new Algorithm(context, locationService);
            var info = await algorithm.Find(time, locationFrom, locationTo, countOfPeople, id);
            if (info != null)
                await Save(info, repository);
        }

        private async Task Save(List<int> info, IRepository repository)
        {
            if (info.Count == 3)
                await repository.SavePassengers(info[0], info[1], info[2], 0, 0);
            else if (info.Count == 4)
                await repository.SavePassengers(info[0], info[1], info[2], info[3], 0);
            else
                await repository.SavePassengers(info[0], info[1], info[2], info[3], info[4]);
        }

        public OrderWithClientViewModel GetOrder(int id)
        {
            IRepository repository = new Repository(context);
            ILocationController repository1 = new Locations(locationService, context);
            var locations = repository1.Index();
            var order = repository.GetOrder(id);
            var passengers = repository.GetPassengers(id);
            return new OrderWithClientViewModel { ReadyOrder = order, Clients = passengers, Locations = locations };
        }

        public ReadyOrdersViewModel GetOrdersByClientId(string id)
        {
            ILocationController repository1 = new Locations(locationService, context);
            var locations = repository1.Index();
            IRepository repository = new Repository(context);
            var client = repository.GetClient(id);
            var ordersId = repository.GetOrdersByClientId(client.Id);
            return new ReadyOrdersViewModel { Id = id, OrdersId = ordersId, ReadyOrders = repository.GetOrders()
            , Locations = locations};
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

        public async Task Rating(string name, string whomName, int orderId, int newRating)
        {
            IRating rate = new RatingContext();
            IRepository repository = new Repository(context);
            var id = await repository.GetClientIdByName(name);
            var whomId = await repository.GetClientIdByName(whomName);
            await rate.Create(id, whomId, orderId, newRating);

            var countOfRates = repository.GetCountOfRates(whomId);
            var rating = repository.GetRating(whomId);

            INewRating newRate = new NewRating();
            var newCountOfRates = newRate.GetNewCountOfRates(countOfRates);
            var ratingOfClient = newRate.GetNewRating(countOfRates, rating, newRating);

            await repository.UpdateRating(whomId, newCountOfRates, ratingOfClient);
        }

        public async Task<bool> Find(string name, string whomName, int orderId)
        {
            IRating rate = new RatingContext();
            IRepository repository = new Repository(context);
            var id = await repository.GetClientIdByName(name);
            var whomId = await repository.GetClientIdByName(whomName);
            return rate.Find(id, whomId, orderId);
        }
    }
}
