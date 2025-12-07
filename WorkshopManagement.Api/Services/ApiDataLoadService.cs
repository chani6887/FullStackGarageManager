using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MongoDB.Driver.Core.Misc;
using SharpCompress.Common;
using WorkshopManagement.Api.Models;
using WorkshopManagement.Api.Models.Gov;
using WorkshopManagement.Api.Models.Mongo;
using static System.Net.Mime.MediaTypeNames;

namespace WorkshopManagement.Api.Services
{
    public class ApiDataLoadService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://data.gov.il/api/3/action/datastore_search?resource_id=bb68386a-a331-4bbc-b668-bba2766d517d&limit=32000";
        private static readonly TimeSpan UpdateInterval = TimeSpan.FromHours(12);

        public ApiDataLoadService(IServiceProvider serviceProvider, HttpClient httpClient)
        {
            _serviceProvider = serviceProvider;
            _httpClient = httpClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await LoadAndSaveDataAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(UpdateInterval, stoppingToken);

                await LoadAndSaveDataAsync();
            }
        }

        private async Task LoadAndSaveDataAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<IWorkshopRepository>();

                try
                {
                    var response = await _httpClient.GetAsync(ApiUrl);
                    response.EnsureSuccessStatusCode();

                    var jsonString = await response.Content.ReadAsStringAsync();

                    var apiResponse = JsonSerializer.Deserialize<GovApiWorkshopResponse>(
                        jsonString,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                    if (apiResponse?.Result?.Records != null && apiResponse.Result.Records.Any())
                    {
                        var records = apiResponse.Result.Records;


                        var data = records.Select(S=> new WorkshopData() { 
                            Address = S.Address,
                            City = S.City,
                            RecordId = S.RecordId,
                            WorkshopName = S.WorkshopName,
                            WorkshopNumber  = S.WorkshopNumber,
                        }).ToList();



                        await repository.InsertManyOrUpdateFullAsync(data);

                    }
                }
                catch (Exception ex)
                {
                    //LOG....
                }
            }
        }
    }
}
