namespace WorkshopManagement.Api.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WorkshopManagement.Api.Models.Mongo;

    public interface IWorkshopRepository
    {
        Task<IEnumerable<WorkshopData>> GetAllAsync();

        Task AddAsync(WorkshopData workshop);

        Task InsertManyOrUpdateFullAsync(List<WorkshopData> newWorkshops);
    }
}
