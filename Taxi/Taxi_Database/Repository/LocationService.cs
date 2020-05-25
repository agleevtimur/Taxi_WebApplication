using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi_Database.Context;
using Taxi_Database.Models;

namespace Taxi_Database.Repository
{
    public class LocationService : ILocationService
    {
        private ApplicationContext db;
        private IMemoryCache cache;
        public LocationService(ApplicationContext context, IMemoryCache memoryCache)
        {
            db = context;
            cache = memoryCache;
        }

        public async Task SaveLocation(Location location)
        {
            db.Location.Add(location);
            int n = await db.SaveChangesAsync();
            if (n > 0)
            {
                cache.Set(location.NameOfLocation, location, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });
            }
        }

        public async Task<int> GetLocationId(string location)
        {
            Location newLocation = null;
            if (!cache.TryGetValue(location, out newLocation))
            {
                newLocation = await db.Location.Where(x => x.NameOfLocation == location)
                    .FirstOrDefaultAsync();
                if (newLocation != null)
                {
                    cache.Set(newLocation.NameOfLocation, newLocation,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            return newLocation.Id;
        }

        public async Task<Location> GetLocation(string location)
        {
            Location newLocation = null;
            if (!cache.TryGetValue(location, out newLocation))
            {
                newLocation = await db.Location.Where(x => x.NameOfLocation == location)
                    .FirstOrDefaultAsync();
                if (newLocation != null)
                {
                    cache.Set(newLocation.NameOfLocation, newLocation,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            return newLocation;
        }

        public string GetLocationNameById(int id)
        {
            return db.Location.Find(id).NameOfLocation;
        }

        public IEnumerable<Location> GetLocations()
        {
            var locations = db.Location.Select(x => x);
            return locations;
        }
    }
}
