using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Backend.Database.Models
{
    public class Card
    {
      

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // This field references the group to which the card belongs
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfNull] // MongoDB will ignore this field if null
        [JsonIgnore] // Prevents the field from being required in the request body
        public string? GroupID { get; set; } // Reference to the Group

        public string Question { get; set; } // "2 + 2 = ?"
        public string Answer { get; set; } // "4"
      
    }
}
