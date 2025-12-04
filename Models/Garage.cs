// Models/Garage.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Garage
{
	// מפתח ראשי ב-MongoDB
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string? Id { get; set; }

	// שדה לבדיקת כפילויות (Unique Key)
	[BsonElement("mispar_rishayon")]
	public required string Mispar_Rishayon { get; set; }

	[BsonElement("shem_mosach")]
	public required string Shem_Mosach { get; set; }

	[BsonElement("ktovet")]
	public required string Ktovet { get; set; }

	[BsonElement("yishuv")]
	public required string Yishuv { get; set; }
}