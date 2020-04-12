using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Models
{
    public class HistoryOfOrder
    {
        public int Id { get; set; }
        public int StartPointId { get; set; }
        public int FinishPointId { get; set; }
        public DateTime CompleteTime { get; set; }
        public DateTime OrderTime { get; set; }
        public int[] ClientsId { get; set; }
    }
}
