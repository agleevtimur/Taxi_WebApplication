using System;

namespace BusinessLogic.ModelsForControllers
{
    public class CardOrder
    {
        public CardOrder(int id, string startPoint, string finishPoint, DateTime departureTime, DateTime orderTime, int priority, int userId)
        {
            Id = id;
            StartPoint = startPoint;
            FinishPoint = finishPoint;
            DepartureTime = departureTime;
            OrderTime = orderTime;
            Priority = priority;
            UserId = userId;
        }

        public int Id { get; set; }
        public string StartPoint { get; set; }
        public string FinishPoint { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime OrderTime { get; set; }
        public int Priority { get; set; }
        public int UserId { get; set; }
    }

}
