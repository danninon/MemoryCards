using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Backend.Database.Models
{
    public class Card
    {
      
        public Card()
        {
            CorrectAttempts = 0;
            TotalAttempts = 0;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        // This field references the group to which the card belongs
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull] // MongoDB will ignore this field if null
        [JsonIgnore] // Prevents the field from being required in the request body
        public string? GroupID { get; set; } // Reference to the Group


        public required string Question { get; set; } // "2 + 2 = ?" one day this should be objects other than strings
        public required string Answer { get; set; } // "4"

        public int CorrectAttempts { get; set; }
        public int TotalAttempts { get; set; }
        public DateTime LastUpdated { get; set; }
        //public DateTime lastTimeRetrieved { get; set; }

    }
}
