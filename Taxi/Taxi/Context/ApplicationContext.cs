using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Models;

namespace Taxi.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Client { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<HistoryOfOrder> HistoryOfOrder { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<HistoryOfLocation> HistoryOfLocation { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
             : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
