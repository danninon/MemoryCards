using Backend.Database.Models;

namespace Backend.Business
{
    public interface IDBService
    {
        void Add(List<Card> cards);
        void DeleteGroup(string groupName);
        IEnumerable<Card> GetGroup(string groupName);
        IEnumerable<string> GetGroupNames();

        void updateCard(Card card, bool didSucceed);

    }
}
