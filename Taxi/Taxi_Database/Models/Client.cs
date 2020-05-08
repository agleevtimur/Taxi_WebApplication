using System;

namespace Taxi_Database.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Rating { get; set; }
        public int CountOfTrips { get; set; }
        public int Role { get; set; }
        public int Priority { get; set; }
        public int LeftOrdersPriority { get; set; }
        public string AboutSelf { get; set; }
        public int CountOfRates { get; set; }
        public DateTime RegisterTime { get; set; }
    }
}
