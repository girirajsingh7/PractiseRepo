using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOutletReview.Models;
namespace FoodOutletReview.Services
{

    public class MongoDbService
    {
        private readonly IMongoCollection<FoodOutlet> _collection;
        private readonly ILogger<MongoDbService> _logger;

        public MongoDbService(IOptions<MongoDbSettings> mongoOptions, ILogger<MongoDbService> logger)
        {
            var MDBsettings = mongoOptions.Value;
            var client = new MongoClient(MDBsettings.ConnectionString);
            var database = client.GetDatabase(MDBsettings.DatabaseName);
            _collection = database.GetCollection<FoodOutlet>(MDBsettings.CollectionName);
            _logger = logger;
        }

            
        public async Task InsertOneAsync(FoodOutlet document)
        {
            try
            {
                await _collection.InsertOneAsync(document);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting one document into MongoDB collection");
            }
        }

        public async Task InsertManyAsync(List<FoodOutlet> documents)
        {
            try
            {
                await _collection.InsertManyAsync(documents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting multiple documents into MongoDB collection");
            }
        }

        public async Task<List<FoodOutlet>> FindAsync(FilterDefinition<FoodOutlet> filter)
        {
            try
            {
                return await _collection.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error finding documents in MongoDB collection");
                return new List<FoodOutlet>();
            }
        }

        public async Task UpdateOneAsync(FilterDefinition<FoodOutlet> filter, UpdateDefinition<FoodOutlet> update)
        {
            try
            {
                await _collection.UpdateOneAsync(filter, update);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating one document in MongoDB collection");
            }
        }

        public async Task UpdateManyAsync(FilterDefinition<FoodOutlet> filter, UpdateDefinition<FoodOutlet> update)
        {
            try
            {
                await _collection.UpdateManyAsync(filter, update);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating multiple documents in MongoDB collection");
            }
        }

        public async Task DeleteOneAsync(FilterDefinition<FoodOutlet> filter)
        {
            try
            {
                await _collection.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting one document from MongoDB collection");
            }
        }

        public async Task DeleteManyAsync(FilterDefinition<FoodOutlet> filter)
        {
            try
            {
                await _collection.DeleteManyAsync(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting multiple documents from MongoDB collection");
            }
        }

        public async Task<long> CountDocumentsAsync(FilterDefinition<FoodOutlet> filter)
        {
            try
            {
                return await _collection.CountDocumentsAsync(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error counting documents in MongoDB collection");
                return 0;
            }
        }
    }


}
