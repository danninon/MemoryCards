using Backend.Business;
using Backend.Database.Models;
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
        public GroupController(
            IGroupRepository groupRepository,
            ICardRepository cardRepository,
            ILogger<GroupController> logger // Inject the logger)
        ){
            _groupRepository = groupRepository;
            _cardRepository = cardRepository;
            _logger = logger; // Assign the logger
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


        [HttpPost("{groupId}/cards")]
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



        // Other group-related actions...
    }
}
