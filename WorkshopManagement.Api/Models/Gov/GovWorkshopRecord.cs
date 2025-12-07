using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization; 

namespace WorkshopManagement.Api.Models.Mongo
{
    public class GovWorkshopRecord
    {
        [JsonPropertyName("_id")]
        public int RecordId { get; set; }

        [JsonPropertyName("mispar_mosah")]
        public int WorkshopNumber { get; set; }

        [JsonPropertyName("shem_mosah")]
        public string WorkshopName { get; set; }

        [JsonPropertyName("cod_sug_mosah")]
        public int WorkshopTypeCode { get; set; }

        [JsonPropertyName("sug_mosah")]
        public string WorkshopType { get; set; }

        [JsonPropertyName("ktovet")]
        public string Address { get; set; }

        [JsonPropertyName("yishuv")]
        public string City { get; set; }

        [JsonPropertyName("telephone")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("mikud")]
        public int ZipCode { get; set; }

        [JsonPropertyName("cod_miktzoa")]
        public int ProfessionCode { get; set; }

        [JsonPropertyName("miktzoa")]
        public string Profession { get; set; }

        [JsonPropertyName("menahel_miktzoa")]
        public string ProfessionManager { get; set; }

        [JsonPropertyName("rasham_havarot")]
        public long CompaniesRegistrar { get; set; }

        [JsonPropertyName("TESTIME")]
        public string TestTime { get; set; }
    }
}