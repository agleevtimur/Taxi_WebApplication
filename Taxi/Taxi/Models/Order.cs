using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int StartPointId { get; set; }
        public int FinishPointId { get; set; }
        public int CountOfPeople { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime OrderTime { get; set; }
        public int Priority { get; set; }
        public int UserId { get; set; }
    }
}
