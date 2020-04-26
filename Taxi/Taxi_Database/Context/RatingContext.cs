using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi_Database.Models;
using Taxi_Database.Repository;

namespace Taxi_Database.Context
{
    public class RatingContext : IRating
    {
        IMongoCollection<Rating> Rating; // коллекция в базе данных
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
            Rating = database.GetCollection<Rating>("Rating");
        }

        // добавление документа
        public async Task Create(int whoId, int whomId, int orderId, int rating)
        {
            var rate = new Rating(whoId, whomId, orderId, rating);
            await Rating.InsertOneAsync(rate);
        }
    }
}
