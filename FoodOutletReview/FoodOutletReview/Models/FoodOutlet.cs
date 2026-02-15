using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodOutletReview.Models
{
    [BsonIgnoreExtraElements]

    public class FoodOutlet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        [JsonPropertyName("id")]
        public int? idTemp { get; set; }
        public string city { get; set; }
        public string name { get; set; }
        public int estimated_cost { get; set; }
        public UserRating user_rating { get; set; }
    }
    
    public class UserRating
    {
        public double average_rating { get; set; }
        public int votes { get; set; }
    }
}
