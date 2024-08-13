using Backend.Business.Repositories;
using Backend.Database.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ILogger<GroupController> _logger;
        private readonly CardSelectionService _cardSelectionService;

        public GroupController(
            IGroupRepository groupRepository,
            ICardRepository cardRepository,
            ILogger<GroupController> logger, // Inject the logger)
            CardSelectionService cardSelectionService
        ){
            _groupRepository = groupRepository;
            _cardRepository = cardRepository;
            _logger = logger; // Assign the logger
            _cardSelectionService = cardSelectionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup([FromBody] Group group)
        {
            // Log the received group data
            _logger.LogDebug($"Received request to add a new group. Group details: Name={group?.Name}, Id={group?.Id}");

            if (group == null || string.IsNullOrEmpty(group.Name))
            {
                _logger.LogWarning("Invalid request: Group data is null or missing a Name.");
                return BadRequest("Group data is invalid.");
            }

            try
            {
                _logger.LogInformation($"Attempting to add a new group with Name={group.Name}.");
                await _groupRepository.AddGroupAsync(group);
                _logger.LogInformation($"Group with Name={group.Name} and Id={group.Id} added successfully.");
                return Ok(new { Message = "Group added successfully", Group = group });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding a group with Name={group.Name}.");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("{groupId}/card")]
        public async Task<IActionResult> AddCardToGroup(string groupId, [FromBody] Card card)
        {
            // Log the input at the beginning of the function
            _logger.LogDebug($"Received request to add a card to group {groupId}. Card details: Id={card?.Id}, Question={card?.Question}, Answer={card?.Answer}");

            if (card == null || string.IsNullOrEmpty(groupId))
            {
                _logger.LogWarning("Invalid request: Card or Group ID is invalid.");
                return BadRequest("Card or Group ID is invalid.");
            }

            // Assign the GroupID from the route parameter
            card.GroupID = groupId;
            _logger.LogDebug($"GroupID {groupId} assigned to the card.");

            // Ensure the Id is provided in the request body
            if (string.IsNullOrEmpty(card.Id))
            {
                _logger.LogWarning("The Id field is required.");
                return BadRequest("The Id field is required.");
            }

            try
            {
                _logger.LogInformation($"Attempting to add card with Id {card.Id} to group {groupId}.");
                await _cardRepository.AddCardToGroupAsync(card);
                _logger.LogInformation($"Card with Id {card.Id} successfully added to group {groupId}.");
                return Ok(new { Message = "Card added to group successfully", Card = card });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while adding card with Id {card.Id} to group {groupId}.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            _logger.LogInformation("Received request to retrieve all groups with their associated cards.");

            try
            {
                var groups = await _groupRepository.GetAllGroupsAsync();

                if (groups == null || !groups.Any())
                {
                    _logger.LogWarning("No groups found in the database.");
                    return Ok("No groups found.");
                }

                var result = new List<object>();

                foreach (var group in groups)
                {
                    var cards = new List<Card>();
                    foreach (var cardId in group.CardIds)
                    {
                        var card = await _cardRepository.GetCardByIdAsync(cardId);
                        if (card != null)
                        {
                            cards.Add(card);
                        }
                    }

                    result.Add(new
                    {
                        group.Id,
                        group.Name,
                        Cards = cards
                    });
                }

                _logger.LogInformation($"Successfully retrieved {groups.Count()} groups with their associated cards.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving groups.");
                return StatusCode(500, "An error occurred while retrieving groups.");
            }
        }

        [HttpPost("draw-cards")]
        public async Task<IActionResult> GetSelectedCards([FromBody] DrawCardsRequest request)
        {
            // Log the start of the process
            _logger.LogInformation("Received request to draw cards.");

            try
            {
                // Use the CardSelectionService to perform the card selection logic
                var selectedCards = await _cardSelectionService.SelectCardsAsync(request.GroupIds, request.NumberOfCards);

                // Return the selected cards as the response
                return Ok(selectedCards);
            }
            catch (Exception ex)
            {
                // Log and handle any exceptions that occur during the process
                _logger.LogError(ex, "An error occurred while selecting cards.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



        // Split into two gro

        public class DrawCardsRequest
    {
        public List<string> GroupIds { get; set; }
        public int NumberOfCards { get; set; }
       
    }

        // Other group-related actions...
    }
}
