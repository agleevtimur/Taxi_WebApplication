using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taxi.Models;

namespace Taxi.Context
{
    public class RatingContext
    {
        IGridFSBucket gridFS;   // файловое хранилище
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
            // получаем доступ к файловому хранилищу
            gridFS = new GridFSBucket(database);
            // обращаемся к коллекции Products
            Rating = database.GetCollection<Rating>("Rating");
        }

        // добавление документа
        public async Task Create(Rating r)
        {
            await Rating.InsertOneAsync(r);
        }
    }
}
