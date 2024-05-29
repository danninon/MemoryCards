using MongoDB.Driver;
using MemoryCards.Database.Models;

namespace MemoryCards.Database
{
    public interface IDbClient
    {
        IMongoCollection<Card> GetCardsCollection();
    }
}
