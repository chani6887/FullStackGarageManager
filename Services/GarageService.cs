using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WorkshopManagement.Api.Models;

namespace WorkshopManagement.Api.Services
{
	public class GarageService
	{
		private readonly IMongoCollection<Garage> _garages;

		public GarageService(IOptions<MongoDBSettings> options)
		{
			var settings = options.Value;
			var client = new MongoClient(settings.ConnectionString);
			var db = client.GetDatabase(settings.DatabaseName);
			_garages = db.GetCollection<Garage>("Garages");
		}

		public async Task<List<Garage>> GetAllAsync()
		{
			return await _garages.Find(_ => true).ToListAsync();
		}

		public async Task AddManyAsync(IEnumerable<Garage> garages)
		{
			if (garages == null) return;
			await _garages.InsertManyAsync(garages);
		}

		public async Task<bool> ExistsByExternalIdAsync(string externalId)
		{
			if (string.IsNullOrEmpty(externalId)) return false;
			var count = await _garages.CountDocumentsAsync(g => g.ExternalId == externalId);
			return count > 0;
		}

		// בדיקה לפי שם+כתובת לשמירת כפילויות
		public async Task<bool> ExistsByNameAndAddressAsync(string name, string address)
		{
			var count = await _garages.CountDocumentsAsync(g => g.Name == name && g.Address == address);
			return count > 0;
		}
	}
}
