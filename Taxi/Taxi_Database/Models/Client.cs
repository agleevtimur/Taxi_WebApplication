using System;

namespace Taxi_Database.Models
{
    public class Client
    {
        public Client(string stringId, string firstName, string secondName, string clientName, string email,
            string password, double rating, int countOfTrips, int priority, int leftOrdersPriority,
            string aboutSelf, int countOfRates, DateTime registerTime)
        {
            StringId = stringId;
            FirstName = firstName;
            SecondName = secondName;
            ClientName = clientName;
            Email = email;
            Password = password;
            Rating = rating;
            CountOfTrips = countOfTrips;
            Priority = priority;
            LeftOrdersPriority = leftOrdersPriority;
            AboutSelf = aboutSelf;
            CountOfRates = countOfRates;
            RegisterTime = registerTime;
        }

        public int Id { get; set; }
        public string StringId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public double Rating { get; set; }
        public int CountOfTrips { get; set; }
        public int Priority { get; set; }
        public int LeftOrdersPriority { get; set; }
        public string AboutSelf { get; set; }
        public int CountOfRates { get; set; }
        public DateTime RegisterTime { get; set; }
    }
}
