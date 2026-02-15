using FoodOutletReview.Models;
using FoodOutletReview.Services;
using MongoDB.Driver;
using FoodOutletReview.Models;
using FoodOutletReview.Services;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc;


namespace FoodOutletReview.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodOutletController : ControllerBase
    {
        private readonly MongoDbService _mongoService;

        public FoodOutletController(MongoDbService mongoService)
        {
            _mongoService = mongoService;
        }

        // GET: api/FoodOutlet
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var outlets = await _mongoService.FindAsync(FilterDefinition<FoodOutlet>.Empty);
            return Ok(outlets);
        }

        // GET: api/FoodOutlet/{id}
        [HttpGet("getoutletbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var filter = Builders<FoodOutlet>.Filter.Eq(f => f.idTemp, id);
            var outlet = await _mongoService.FindAsync(filter);
            if (outlet == null || outlet.Count == 0) return NotFound();
            return Ok(outlet);
        }

        // POST: api/FoodOutlet
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] FoodOutlet outlet)
        {
            await _mongoService.InsertOneAsync(outlet);
            return CreatedAtAction(nameof(GetById), new { id = outlet.idTemp }, outlet);
        }

        // PUT: api/FoodOutlet/{id}
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FoodOutlet updatedOutlet)
        {
            var filter = Builders<FoodOutlet>.Filter.Eq(f => f.idTemp, id);
            var update = Builders<FoodOutlet>.Update
                .Set(f => f.city, updatedOutlet.city)
                .Set(f => f.name, updatedOutlet.name)
                .Set(f => f.user_rating, updatedOutlet.user_rating); 
            await _mongoService.UpdateOneAsync(filter, update);
            return Ok("Data updated successfully");
        }

        // DELETE: api/FoodOutlet/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var filter = Builders<FoodOutlet>.Filter.Eq(f => f.idTemp, id);
            await _mongoService.DeleteOneAsync(filter);
            return Ok("Data deleted successfully");
        }

    }
}
