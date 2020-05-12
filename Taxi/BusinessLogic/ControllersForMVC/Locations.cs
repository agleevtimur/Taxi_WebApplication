using System.Collections.Generic;
using System.Threading.Tasks;
using Taxi_Database.Context;
using Taxi_Database.Models;
using Taxi_Database.Repository;

namespace BusinessLogic.ControllersForMVC
{
    public class Locations : ILocationController
    {
        private readonly ApplicationContext context;

        public Locations(ApplicationContext context)
        {
            this.context = context;
        }

        public IEnumerable<Location> Index()
        {
            IRepository repository = new Repository(context);
            return repository.GetLocations();
        }

        public IEnumerable<HistoryOfLocation> History()
        {
            IRepository repository = new Repository(context);
            return repository.GetHistoryOfLocations();
        }

        public async Task SavePost(string name, string googleCode, string yandexCode, string twoGisCode)
        {
            IRepository repository = new Repository(context);
            await repository.SaveLocation(new Location(name, googleCode, yandexCode, twoGisCode));
        }
    }
}
