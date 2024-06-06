using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Database.Models
{
    public class Card
    {
        public Card()
        {
            // Initialize with default values
            Attempts = 0;
            CorrectAttempts = 0;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? Id { get; set; }
        [BsonElement("Name")]
        public string GroupName { get; set; } // "Math for kids"
        public string Question { get; set; } // "2 + 2 = ?"
        public string Answer { get; set; } // "4"
        public int Attempts { get; set; }
        public int CorrectAttempts { get; set; } 
    }


}
