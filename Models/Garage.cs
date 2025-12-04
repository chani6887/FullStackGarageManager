using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WorkshopManagement.Api.Models
{
	public class Garage
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }

		[BsonElement("name")]
		public string Name { get; set; } = "";

		[BsonElement("city")]
		public string City { get; set; } = "";

		[BsonElement("address")]
		public string Address { get; set; } = "";

		// מזהה מה-API הממשלתי (אם קיים) — חשוב לבדוק מיפוי מה-response
		[BsonElement("externalId")]
		public string? ExternalId { get; set; }
	}
}
