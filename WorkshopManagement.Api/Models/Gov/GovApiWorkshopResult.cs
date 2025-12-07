using WorkshopManagement.Api.Models.Mongo;

namespace WorkshopManagement.Api.Models.Gov
{
    public class GovApiWorkshopResult
    {
        public required List<GovWorkshopRecord> Records { get; set; }
    }
}
