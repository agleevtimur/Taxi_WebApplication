using BusinessLogic.ModelsForControllers.Home;
using Taxi_Database.Context;
using Taxi_Database.Repository;

namespace BusinessLogic.ControllersForMVC
{
    public class Home : IHomeController
    {
        private readonly ApplicationContext context;

        public Home(ApplicationContext context)
        {
            this.context = context;
        }

        public IndexHomeViewModel Index()
        {
            IRepository repository = new Repository(context);

            return new IndexHomeViewModel
            {
                CountOfСlients = repository.CountOfClients(),
                CountOfReadyOrders = repository.CountOdReadyOrders(),
                CountOfSubcriptionNow = repository.CountOfSubscription(),
                CountOfNewClientsInMonth = repository.CountOfClientsInMonth(),
                CountOfReadyOrdersInDay = repository.CountOfReadyOrdersInDay()
            };
        }
    }
}
