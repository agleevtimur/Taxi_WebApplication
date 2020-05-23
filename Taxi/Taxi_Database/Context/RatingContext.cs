using MongoDB.Driver;
using System.Text.Json;
using System.Threading.Tasks;
using Taxi_Database.Models;
using Taxi_Database.Repository;

namespace Taxi_Database.Context
{
    public class RatingContext : IRating
    {
        IMongoCollection<string> Rating; // коллекция в базе данных
        public RatingContext()
        {
            // строка подключения
            string connectionString = "mongodb://localhost:27017/ratingstore";
            var connection = new MongoUrlBuilder(connectionString);
            // получаем клиента для взаимодействия с базой данных
            MongoClient client = new MongoClient(connectionString);
            // получаем доступ к самой базе данных
            IMongoDatabase database = client.GetDatabase(connection.DatabaseName);
            // обращаемся к коллекции Products
            Rating = database.GetCollection<string>("Rating");
        }

        // добавление документа
        public async Task Create(string whoId, string whomId, int orderId, int rating)
        {
            var rate = new Rating(whoId, whomId, orderId, rating);
            string json = JsonSerializer.Serialize<Rating>(rate);
            await Rating.InsertOneAsync(json);
        }
    }
}
