using Microsoft.AspNetCore.Mvc;
using MemoryCards.Business;
using MemoryCards.Database.Models;


namespace MemoryCards.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudyGroupsController : ControllerBase
    {
        private readonly ILogger<StudyGroupsController> _logger;
        private readonly IStudyGroupService _studyGroupService;

        public StudyGroupsController(IStudyGroupService studyGroupService, ILogger<StudyGroupsController> logger)
        {
            _logger = logger;
            _studyGroupService = studyGroupService;
        }

        [HttpPost]
        public IActionResult CreateGroup(List<Card> cards)
        {
            try
            {
                _studyGroupService.Add(cards);
                _logger.LogInformation("Created a new group with {NumberOfCards} cards", cards.Count);
                foreach (var card in cards)
                {
                    _logger.LogInformation("Added card with details: {CardDetails}", card.ToString());
                }
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create group");
                return StatusCode(500, "Failed to create group");
            }
        }

        [HttpGet("{groupName}")]
        public IActionResult GetGroup(string groupName)
        {
            try
            {
                var group = _studyGroupService.GetGroup(groupName);
                if (group == null)
                {
                    _logger.LogWarning("Group {GroupName} not found", groupName);
                    return NotFound();
                }
                return Ok(group);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve group {GroupName}", groupName);
                return StatusCode(500, "Failed to retrieve group");
            }
        }

        [HttpGet("group-names")]
        public IActionResult GetGroupNames()
        {
            try
            {
                List<string> groupNames = _studyGroupService.GetGroupNames().ToList();
                _logger.LogInformation($"Retrieved {groupNames.Count} group names");
                return Ok(groupNames);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve group names");
                return StatusCode(500, "Failed to retrieve group names");
            }
        }

        [HttpDelete("{groupName}")]
        public IActionResult DeleteGroup(string groupName)
        {
            try
            {
                _studyGroupService.DeleteGroup(groupName);
                _logger.LogInformation("Deleted group {GroupName}", groupName);
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete group {GroupName}", groupName);
                return StatusCode(500, "Failed to delete group");
            }
        }
    }
}
