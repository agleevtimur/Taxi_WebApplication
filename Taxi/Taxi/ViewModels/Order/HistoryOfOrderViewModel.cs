using System;

namespace Taxi.ViewModels.Order
{
    public class HistoryOfOrderViewModel
    {
        public string StartPoint { get; set; }
        public string FinishPoint { get; set; }
        public DateTime CompleteTime { get; set; }
        public DateTime OrderTime { get; set; }
        public string Login1 { get; set; }
        public int Rating1 { get; set; }
        public string Login2 { get; set; }
        public int Rating2 { get; set; }
        public string Login3 { get; set; }
        public int Rating3 { get; set; }
        public string Login4 { get; set; }
        public int Rating4 { get; set; }

    }
}
