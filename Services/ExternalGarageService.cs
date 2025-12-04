using System.Net.Http.Json;
using System.Text.Json;
using WorkshopManagement.Api.Models;

namespace WorkshopManagement.Api.Services
{
	public class ExternalGarageService
	{
		private readonly HttpClient _http;
		public ExternalGarageService(HttpClient http) => _http = http;

		public async Task<List<Garage>> FetchFromGovAsync(int limit = 5)
		{
			var url = $"https://data.gov.il/api/3/action/datastore_search?resource_id=bb68386a-a331-4bbc-b668-bba2766d517d&limit={limit}";
			var resp = await _http.GetFromJsonAsync<JsonElement>(url);
			var list = new List<Garage>();

			if (resp.TryGetProperty("result", out var result) && result.TryGetProperty("records", out var records))
			{
				foreach (var rec in records.EnumerateArray())
				{
				
					string name = rec.TryGetProperty("שם_מוסך", out var n) ? n.GetString() ?? "" : (rec.TryGetProperty("שם", out var n2) ? n2.GetString() ?? "" : "");
					string city = rec.TryGetProperty("יישוב", out var c) ? c.GetString() ?? "" : "";
					string address = rec.TryGetProperty("כתובת", out var a) ? a.GetString() ?? "" : "";
					string externalId = rec.TryGetProperty("_id", out var id) ? id.ToString() : null;

					list.Add(new Garage
					{
						Name = name,
						City = city,
						Address = address,
						ExternalId = externalId
					});
				}
			}
			return list;
		}
	}
}
