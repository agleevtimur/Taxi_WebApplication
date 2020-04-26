using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi_Database.Models
{
    public class HistoryOfOrder
    {
        public HistoryOfOrder(int startPointId, int finishPointId, DateTime completeTime, 
            DateTime orderTime, int[] clientsId)
        {
            StartPointId = startPointId;
            FinishPointId = finishPointId;
            CompleteTime = completeTime;
            OrderTime = orderTime;
            ClientsId = clientsId;
        }

        public int Id { get; set; }
        public int StartPointId { get; set; }
        public int FinishPointId { get; set; }
        public DateTime CompleteTime { get; set; }
        public DateTime OrderTime { get; set; }
        public int[] ClientsId { get; set; }
    }
}
