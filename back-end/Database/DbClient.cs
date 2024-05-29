using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MemoryCards.Database.Models;

namespace MemoryCards.Database
{
    public class DbClient : IDbClient
    {
        private readonly IMongoDatabase _database;

        public DbClient(IOptions<DbConfig> dbConfig)
        {
            MongoClient client = new MongoClient(dbConfig.Value.ConnectionString);
            _database = client.GetDatabase(dbConfig.Value.DatabaseName);

        }

        public  IMongoCollection<Card> GetCardsCollection()
        {
            return _database.GetCollection<Card>("Cards"); //Cards is the collection's designated name, like a table in SQL
        }
    }
}
