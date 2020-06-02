using System;
using System.Collections.Generic;
using System.Text;
using Taxi_Database.Models;

namespace BusinessLogic.ModelsForControllers
{
    public class IndexOrderViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<ReadyOrders> ReadyOrders { get; set; }
    }
}
