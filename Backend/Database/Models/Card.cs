using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Database.Models
{
    public class Card
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? Id { get; set; }
        [BsonElement("Name")]
        public string GroupName { get; set; } // "Math for kids"
        public string Front { get; set; } // "2 + 2 = ?"
        public string Back { get; set; } // "4"

    }
}
