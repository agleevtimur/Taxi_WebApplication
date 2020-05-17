using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Taxi_Database.Models
{
    public class Rating
    {
        public Rating(string whoId, string whomId, int orderId, int countOfStars)
        {
            WhoId = whoId;
            WhomId = whomId;
            OrderId = orderId;
            CountOfStars = countOfStars;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string WhoId { get; set; }
        public string WhomId { get; set; }
        public int OrderId { get; set; }
        public int CountOfStars { get; set; }
    }
}
