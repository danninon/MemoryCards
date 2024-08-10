using Backend.Database.Models;


namespace Backend.Business
{
    public interface IGroupRepository
    {
        Task<Group> GetGroupByIdAsync(string groupId);
        Task AddGroupAsync(Group group);
        Task UpdateGroupAsync(Group group);
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        // Other group-related methods...
    }
}
