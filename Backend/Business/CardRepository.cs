using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Backend.Database;
using Backend.Database.Models;

namespace Backend.Business
{
    public class CardRepository : ICardRepository
    {
        private IMongoCollection<Card> _cardsCollection;

        public CardRepository(IDbClient dbClient) {
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

        public Card getCardById(string cardId)
        {
            return _cardsCollection.Find(card => card.Id == cardId).FirstOrDefault();
        }

        public void updateCard(string cardId, bool didSucceed)
        {
            Card card =  _cardsCollection.Find(card => card.Id == cardId).FirstOrDefault();
            card.CorrectAttempts = didSucceed ? card.CorrectAttempts+1:card.CorrectAttempts;
            card.Attempts = card.Attempts + 1;
            _cardsCollection.ReplaceOne(c => c.Id == card.Id, card);
        }
    }
}