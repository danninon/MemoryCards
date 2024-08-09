using Backend.Database;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Backend.Controllers
{
    public class HealthController : Controller
    {
        private readonly IDbClient _dbClient;
        public HealthController(IDbClient dbClient)
        {
            _dbClient = dbClient;
        }

        [HttpGet("health/mongo")]
        public async Task<IActionResult> CheckMongoHealth()
        {
            try
            {
                var collection = _dbClient.GetCardsCollection();
                await collection.Find(_ => true).Limit(1).ToListAsync(); // A simple query to check connection
                return Ok("MongoDB is healthy.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"MongoDB health check failed: {ex.Message}");
            }
        }
    }
}
