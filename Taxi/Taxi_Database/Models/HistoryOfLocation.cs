namespace Taxi_Database.Models
{
    public class HistoryOfLocation
    {
        public HistoryOfLocation(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CountOfDepartures { get; set; }
        public int CountOfArrivals { get; set; }
    }
}
