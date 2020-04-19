using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taxi.Models
{
    public class Rating
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int WhoId { get; set; }
        public int WhomId { get; set; }
        public int OrderId { get; set; }
        public int CountOfStars { get; set; }
    }
}
