using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;
using Backend.Database.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.Business.Repositories;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ICardRepository _cardRepository;
        private readonly ILogger<TestController> _logger;

        public TestController(
            IGroupRepository groupRepository,
            ICardRepository cardRepository,
            ILogger<TestController> logger)
        {
            _groupRepository = groupRepository;
            _cardRepository = cardRepository;
            _logger = logger;
        }

        [HttpPost("add-hardcoded-groups")]
        public async Task<IActionResult> AddHardcodedGroups()
        {
            _logger.LogInformation("Received request to add hardcoded groups.");

            await AddGroup1Async();
            await AddGroup2Async();
            await AddGroup3Async();

            return Ok("Hardcoded groups and cards added successfully.");
        }

        private async Task AddGroup1Async()
        {
            var group1 = new Group
            {
                Id = "66bb515505099bb8ac87356c",
                Name = "Math for Beginners",
                CardIds = new List<string>()
            };
            await _groupRepository.AddGroupAsync(group1);
            _logger.LogInformation($"Group '{group1.Name}' added successfully.");

            await AddCard1Async(group1.Id);
            await AddCard2Async(group1.Id);
        }

        private async Task AddGroup2Async()
        {
            var group2 = new Group
            {
                Id = "66bb515505099bb8ac87356d",
                Name = "Math for Associates",
                CardIds = new List<string>()
            };
            await _groupRepository.AddGroupAsync(group2);
            _logger.LogInformation($"Group '{group2.Name}' added successfully.");

            await AddCard3Async(group2.Id);
            await AddCard4Async(group2.Id);
            await AddCard5Async(group2.Id);
        }

        private async Task AddGroup3Async()
        {
            var group3 = new Group
            {
                Id = "66bb515505099bb8ac87356e",
                Name = "Math for Advanced",
                CardIds = new List<string>()
            };
            await _groupRepository.AddGroupAsync(group3);
            _logger.LogInformation($"Group '{group3.Name}' added successfully.");

            await AddCard6Async(group3.Id);
        }

        private async Task AddCard1Async(string groupId)
        {
            var card1 = new Card
            {
                Id = ObjectId.GenerateNewId().ToString(), // Generate a new BsonId
                Question = "What is 1 + 1?",
                Answer = "2",
                GroupID = groupId
            };
            await _cardRepository.AddCardToGroupAsync(card1);
       
            card1.CorrectAttempts = 1;
            card1.TotalAttempts = 1;
            //card1.LastUpdated = DateTime.UtcNow;
            await _cardRepository.UpdateCardAsync(card1);
            _logger.LogInformation($"Card '{card1.Question}' added to group '{groupId}'.");
        }

        private async Task AddCard2Async(string groupId)
        {
            var card2 = new Card
            {
                Id = ObjectId.GenerateNewId().ToString(), // Generate a new BsonId
                Question = "What is 2 + 2?",
                Answer = "4",
                GroupID = groupId
            };
            card2.CorrectAttempts = 1;
            card2.TotalAttempts = 2;
            await _cardRepository.AddCardToGroupAsync(card2);
            await _cardRepository.UpdateCardAsync(card2);
            _logger.LogInformation($"Card '{card2.Question}' added to group '{groupId}'.");
        }

        private async Task AddCard3Async(string groupId)
        {
            var card3 = new Card
            {
                Id = ObjectId.GenerateNewId().ToString(), // Generate a new BsonId
                Question = "What is exp(2,3)?",
                Answer = "8",
                GroupID = groupId
            };
            card3.CorrectAttempts = 2;
            card3.TotalAttempts = 3;
            await _cardRepository.AddCardToGroupAsync(card3);
            await _cardRepository.UpdateCardAsync(card3);
            _logger.LogInformation($"Card '{card3.Question}' added to group '{groupId}'.");
        }

        private async Task AddCard4Async(string groupId)
        {
            var card4 = new Card
            {
                Id = ObjectId.GenerateNewId().ToString(), // Generate a new BsonId
                Question = "What is (4 + 4) / 2?",
                Answer = "4",
                GroupID = groupId
            };
            card4.CorrectAttempts = 0;
            card4.TotalAttempts = 0;
            await _cardRepository.AddCardToGroupAsync(card4);
            await _cardRepository.UpdateCardAsync(card4);
            _logger.LogInformation($"Card '{card4.Question}' added to group '{groupId}'.");
        }

        private async Task AddCard5Async(string groupId)
        {
            var card5 = new Card
            {
                Id = ObjectId.GenerateNewId().ToString(), // Generate a new BsonId
                Question = "What is sqrt(9,3)?",
                Answer = "2",
                GroupID = groupId
            };
            card5.CorrectAttempts = 1;
            card5.TotalAttempts = 100;
            await _cardRepository.AddCardToGroupAsync(card5);
            await _cardRepository.UpdateCardAsync(card5);
            _logger.LogInformation($"Card '{card5.Question}' added to group '{groupId}'.");
        }

        private async Task AddCard6Async(string groupId)
        {
            var card6 = new Card
            {
                Id = ObjectId.GenerateNewId().ToString(), // Generate a new BsonId
                Question = "What is the derivative of x^2?",
                Answer = "2x",
                GroupID = groupId
            };
            await _cardRepository.AddCardToGroupAsync(card6);
            await _cardRepository.UpdateCardAsync(card6);
            _logger.LogInformation($"Card '{card6.Question}' added to group '{groupId}'.");
        }
    }
}
