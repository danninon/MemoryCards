using Backend.Database.Models;

namespace Backend.Business
{
    //add card
    //update card

    public interface ICardRepository
    {
        //void Add(Card card);

        //void Update(Card card);
        public Task AddCardToGroupAsync(Card card);
        // void Add(List<Card> cards);
        // void DeleteGroup(string groupName);
        // IEnumerable<Card> GetGroup(string groupName);
        //IEnumerable<string> GetGroupNames();
        //public Card getCardById(string cardId);
        //void updateCard(string cardId, bool didSucceed);


    }
}
