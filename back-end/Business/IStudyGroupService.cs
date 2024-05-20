using StudyGroups.WebApi.Database.Models;

namespace StudyGroups.WebApi.Business
{
    public interface IStudyGroupService
    {
        void Add(List<Card> cards);
        void DeleteGroup(string groupName);
        IEnumerable<Card> GetGroup(string groupName);
        IEnumerable<string> GetGroupNames();

        // List<Card> GetAll();
    }
}
