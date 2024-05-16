using System.Collections.Generic;
using Taxi_Database.Models;

namespace BusinessLogic.ModelsForControllers
{
    public class OrderWithClientViewModel
    {
        public ReadyOrders ReadyOrder { get; set; }
        public List<Client> Clients { get; set; }
    }
}
