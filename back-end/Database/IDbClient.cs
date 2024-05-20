using MongoDB.Driver;
using StudyGroups.WebApi.Database.Models;

namespace StudyGroups.WebApi.Database
{
    public interface IDbClient
    {
        IMongoCollection<Card> GetCardsCollection();
    }
}
