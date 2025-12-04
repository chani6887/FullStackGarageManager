// Models/Workshop.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkshopManagement.Api.Models
{
    public class Workshop
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        // המזהה הייחודי של MongoDB - סוג string
        public string Id { get; set; }

        // מזהה המוסך כפי שחוזר מה-API הממשלתי
        public string WorkshopId { get; set; }

        public string WorkshopName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string LicenseType { get; set; }
        public string Phone { get; set; }
    }
}