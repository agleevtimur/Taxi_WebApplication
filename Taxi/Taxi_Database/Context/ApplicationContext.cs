using Microsoft.EntityFrameworkCore;
using Taxi_Database.Models;

namespace Taxi_Database.Context
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Client> Client { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ReadyOrders> ReadyOrders { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<HistoryOfLocation> HistoryOfLocation { get; set; }
        public DbSet<Passengers> Passengers { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
             : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
