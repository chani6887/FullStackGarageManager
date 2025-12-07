using System.Net;
using System.Text.Json.Serialization; 
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkshopManagement.Api.Models.Mongo
{
    public class WorkshopData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonPropertyName("record_id")]
        [BsonElement("record_id")] 
        public int RecordId { get; set; }

        [JsonPropertyName("number")]
        [BsonElement("workshop_number")] 
        public int WorkshopNumber { get; set; }

        [JsonPropertyName("name")]
        [BsonElement("workshop_name")] 
        public string WorkshopName { get; set; }

        [JsonPropertyName("address")]
        [BsonElement("address")] 
        public string Address { get; set; }

        [JsonPropertyName("city")]
        [BsonElement("city")] 
        public string City { get; set; }
 
    }
}
