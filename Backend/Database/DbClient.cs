using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Backend.Database.Models;

namespace Backend.Database
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

        public IMongoCollection<Group> GetGroupsCollection()
        {
            return _database.GetCollection<Group>("Groups"); //Cards is the collection's designated name, like a table in SQL
        }

        //public Card GetCardById(string cardId)
        //{
        //    var cardsCollection = GetCardsCollection();
        //    // Create a filter to search the card by ID
        //    var filter = Builders<Card>.Filter.Eq(card => card.Id, cardId);
        //    // Find the card with the specified ID
        //    var card = cardsCollection.Find(filter).FirstOrDefault();
        //    return card;
        //}
    }
}
