using MongoDB.Driver;
using Backend.Database.Models;

namespace Backend.Database
{
    public interface IDbClient
    {
        IMongoCollection<Card> GetCardsCollection();
        Card GetCardById(string cardId);
    }
}
