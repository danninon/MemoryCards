using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Backend.Database.Models
{
    public class Group
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> CardIds { get; set; } = new List<string>();
        // Other properties...
    }
}
