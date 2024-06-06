using Backend.Database.Models;

namespace Backend.Business
{
    public interface IDBService
    {
        void Add(List<Card> cards);
        void DeleteGroup(string groupName);
        IEnumerable<Card> GetGroup(string groupName);
        IEnumerable<string> GetGroupNames();

        public Card getCardById(string cardId);
        void updateCard(string cardId, bool didSucceed);

        
    }
}
