using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
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

        public IEnumerable<Order> Index()
        {
            IRepository repository = new Repository(context);
            var orders = repository.GetRequests();
            return orders;
        }

        public void Create(string locationFrom, string locationTo, DateTime time, int countOfPeople)
        {
            IRepository repository = new Repository(context);
            //var orderId = repository.SaveOrder(new Order(locationFrom, locationTo, countOfPeople, 
              //  time, DateTime.Now, ))
        }
    }
}
