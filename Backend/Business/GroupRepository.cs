using Backend.Database;
using MongoDB.Driver;
using Backend.Database.Models;

namespace Backend.Business
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IMongoCollection<Group> _groupsCollection;

        public GroupRepository(IDbClient dbClient)
        {
            _groupsCollection = dbClient.GetGroupsCollection();
        }

        public async Task<Group> GetGroupByIdAsync(string groupId)
        {
            return await _groupsCollection.Find(g => g.Id == groupId).FirstOrDefaultAsync();
        }

        public async Task UpdateGroupAsync(Group group)
        {
            await _groupsCollection.ReplaceOneAsync(g => g.Id == group.Id, group);
        }

        public async Task AddGroupAsync(Group group)  // Implementation of the new method
        {
            await _groupsCollection.InsertOneAsync(group);
        }
        // Other methods as needed...
    }

}
