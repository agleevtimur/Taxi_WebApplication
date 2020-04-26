using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi_Database.Models
{
    public class HistoryOfLocation
    {
        public HistoryOfLocation(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public int CountOfDepartures { get; set; }
        public int CountOfArrivals { get; set; }
    }
}
