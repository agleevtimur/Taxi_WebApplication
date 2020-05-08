using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.ViewModels.User
{
    public class ClientViewModel
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public double Raiting { get; set; }
        public int CountOfTravels { get; set; }
        public string AboutSelf { get; set; }
        public int Priority { get; set; }
    }
}
