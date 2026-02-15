using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace FoodOutletReview.Services
{
    public class ApiSyncService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ApiSyncService> _logger;
        private readonly string _filePath =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "outlets.json");

        public ApiSyncService(IServiceScopeFactory scopeFactory,ILogger<ApiSyncService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var apiService = scope.ServiceProvider.GetRequiredService<ApiclientService>();

                var data = await apiService.GetAllOutletsAsync("Seattle");

                var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                await File.WriteAllTextAsync(_filePath, json, stoppingToken);

                _logger.LogInformation("Synced {Count} outlets at {Time}",
                    data.Count, DateTime.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during API sync");
            }

        }
    }

}
