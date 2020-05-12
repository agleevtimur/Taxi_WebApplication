using System;

namespace Taxi_Database.Models
{
    public class Order
    {
        public Order(int startPointId, int finishPointId, int countOfPeople, DateTime departureTime, 
            DateTime orderTime, int priority, int userId)
        {
            StartPointId = startPointId;
            FinishPointId = finishPointId;
            CountOfPeople = countOfPeople;
            DepartureTime = departureTime;
            OrderTime = orderTime;
            Priority = priority;
            UserId = userId;
        }

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
