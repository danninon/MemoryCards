using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Backend.Database;
using Backend.Database.Models;
using Backend.Controllers;

namespace Backend.Business
{
    public class CardRepository : ICardRepository
    {
        //private IMongoCollection<Card> _cardsCollection;

        //public CardRepository(IDbClient dbClient)
        //{
        //    _cardsCollection = dbClient.GetCardsCollection();
        //}

        private readonly IMongoCollection<Card> _cardsCollection;
        private readonly IGroupRepository _groupRepository;

        public CardRepository(
            IDbClient dbClient,
            IGroupRepository groupRepository
        )
        {
            _cardsCollection = dbClient.GetCardsCollection();
            _groupRepository = groupRepository;
        }

        public async Task AddCardToGroupAsync(Card card)
        {
            var group = await _groupRepository.GetGroupByIdAsync(card.GroupID);
            if (group == null)
            {
                throw new Exception("Group not found");
            }

            // Add the card to the group
            group.CardIds.Add(card.Id);
            await _groupRepository.UpdateGroupAsync(group);

            // Finally, add the card to the Cards collection
            await _cardsCollection.InsertOneAsync(card);
        }

        public async Task<Card> GetCardByIdAsync(string cardId)
        {
            // Retrieve the card from the collection using the provided string cardId
            var card = await _cardsCollection.Find(c => c.Id == cardId).FirstOrDefaultAsync();

            return card;
        }

        //public void updateCard(string cardId, bool didSucceed)
        //{
        //    Card card =  _cardsCollection.Find(card => card.Id == cardId).FirstOrDefault();
        //    card.CorrectAttempts = didSucceed ? card.CorrectAttempts+1:card.CorrectAttempts;
        //    card.Attempts = card.Attempts + 1;
        //    _cardsCollection.ReplaceOne(c => c.Id == card.Id, card);
        //}
    }
}