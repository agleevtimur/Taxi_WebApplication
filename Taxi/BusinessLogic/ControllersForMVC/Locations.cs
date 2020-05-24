using System.Collections.Generic;
using System.Threading.Tasks;
using Taxi_Database.Context;
using Taxi_Database.Models;
using Taxi_Database.Repository;

namespace BusinessLogic.ControllersForMVC
{
    public class Locations : ILocationController
    {
        private readonly LocationService locationService;
        private readonly ApplicationContext context;

        public Locations(LocationService locationService, ApplicationContext context)
        {
            this.locationService = locationService;
            this.context = context;
        }

        public IEnumerable<Location> Index()
        {
            return locationService.GetLocations();
        }

        public IEnumerable<HistoryOfLocation> History()
        {
            IRepository repository = new Repository(context);
            return repository.GetHistoryOfLocations();
        }

        public async Task SavePost(string name, string googleCode, string yandexCode, string twoGisCode)
        {
            IRepository repository = new Repository(context);
            await locationService.SaveLocation(new Location(name, googleCode, yandexCode, twoGisCode));
            await repository.SaveHistoryOfLocation(name);
        }

        public async Task<bool> IsInLocations(string locationName)
        {
            var location = await locationService.GetLocation(locationName);
            if (location == null)
                return false;
            else
                return true;
        }
    }
}
