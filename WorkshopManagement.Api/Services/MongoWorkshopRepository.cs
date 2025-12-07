using MongoDB.Driver;
using WorkshopManagement.Api.Models.Mongo;

namespace WorkshopManagement.Api.Services
{
    public class MongoWorkshopRepository : IWorkshopRepository
    {
        private readonly IMongoCollection<WorkshopData> _workshops;
        private const string CollectionName = "Workshops";

        public MongoWorkshopRepository(IMongoClient client, string dbName)
        {
            var database = client.GetDatabase(dbName);
            _workshops = database.GetCollection<WorkshopData>(CollectionName);
        }

        public async Task<IEnumerable<WorkshopData>> GetAllAsync()
        {
            return await _workshops.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(WorkshopData workshop)
        {
            await _workshops.InsertOneAsync(workshop);
        }

        public async Task InsertManyOrUpdateFullAsync(List<WorkshopData> newWorkshops)
        {
            await _workshops.DeleteManyAsync(_ => true);

            if (newWorkshops.Any())
            {
                await _workshops.InsertManyAsync(newWorkshops);
            }
        }
    }
}
