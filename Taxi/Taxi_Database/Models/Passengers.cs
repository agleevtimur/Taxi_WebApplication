namespace Taxi_Database.Models
{
    public class Passengers
    {
        public Passengers(int orderId, int firstId, int secondId, int thirdId, int forthId)
        {
            OrderId = orderId;
            FirstId = firstId;
            SecondId = secondId;
            ThirdId = thirdId;
            ForthId = forthId;
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int FirstId { get; set; }
        public int SecondId { get; set; }
        public int ThirdId { get; set; }
        public int ForthId { get; set; }
    }
}
