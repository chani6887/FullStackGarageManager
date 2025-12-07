namespace WorkshopManagement.Api.Models.Gov
{
    public class GovApiWorkshopResponse
    {
        public required string Help { get; set; }
        public bool Success { get; set; }

        public required GovApiWorkshopResult Result { get; set; }
    }
}
