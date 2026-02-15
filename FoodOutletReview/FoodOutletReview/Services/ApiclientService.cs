using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using FoodOutletReview.Models;

namespace FoodOutletReview.Services
{

    public class ApiclientService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://jsonmock.hackerrank.com/api/food_outlets";

        private readonly MongoDbService _mongoService;
        public ApiclientService(HttpClient httpClient , MongoDbService mongoDbService)
        {
            _httpClient = httpClient;
            _mongoService = mongoDbService;
        }

        public async Task<List<FoodOutlet>> GetAllOutletsAsync(string city)
        {
            int page = 1;
            var results = new List<FoodOutlet>();
            var MongoDb = new List<FoodOutlet>();

            while (true)
            {
                string url = $"{BaseUrl}?city={city}&page={page}";
                var response = await _httpClient.GetFromJsonAsync<FoodOutletResponse>(url);

                if (response == null || response.data == null)
                    break;

                results.AddRange(response.data);
                MongoDb.AddRange(response.data);
                if (page >= response.total_pages)
                    break;

                page++;
            }
            if (MongoDb.Any())
            {
                await _mongoService.InsertManyAsync(MongoDb);
            }
            return results;
        }
    }

}


