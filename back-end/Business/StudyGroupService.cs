using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MemoryCards.Database;
using MemoryCards.Database.Models;

namespace MemoryCards.Business
{
    public class StudyGroupService : IStudyGroupService
    {
        private IMongoCollection<Card> _cardsCollection;

        public StudyGroupService(IDbClient dbClient) {
            _cardsCollection = dbClient.GetCardsCollection();
        }

        public void Add(List<Card> cards)
        {
            _cardsCollection.InsertMany(cards);
        }

        public void DeleteGroup(string groupName)
        {
            _cardsCollection.DeleteOne(cards => cards.GroupName.Equals(groupName));
        }

        public IEnumerable<Card> GetGroup(string groupName) =>
            _cardsCollection.Find(cards => cards.GroupName.Equals(groupName)).ToEnumerable();

        public IEnumerable<string> GetGroupNames() =>
            _cardsCollection.Distinct(cards => cards.GroupName, cards => true).ToEnumerable();
   




        // List<Card> GetAll(){
        //     return _cardsCollection.Find(_ => true).ToList();
        // }
    }
}