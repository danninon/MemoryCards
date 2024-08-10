using Microsoft.AspNetCore.Mvc;
using Backend.Business;
using Backend.Database.Models;


namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ILogger<CardsController> _logger;
        private readonly ICardRepository _dbService;

        public CardsController(ICardRepository dbService, ILogger<CardsController> logger)
        {
            _logger = logger;
            _dbService = dbService;
        }

        //[HttpPost]
        //public IActionResult CreateGroup(List<Card> cards)
        //{
        //    try
        //    {
        //        _dbService.Add(cards);
        //        _logger.LogInformation("Created a new group with {NumberOfCards} cards", cards.Count);
        //        foreach (var card in cards)
        //        {
        //            _logger.LogInformation("Added card with details: {CardDetails}", card.ToString());
        //        }
        //        return StatusCode(201);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Failed to create group");
        //        return StatusCode(500, "Failed to create group");
        //    }
        //}

        //[HttpGet("{groupName}")]
        //public IActionResult GetGroup(string groupName)
        //{
        //    try
        //    {
        //        var group = _dbService.GetGroup(groupName);
        //        if (group == null)
        //        {
        //            _logger.LogWarning("Group {GroupName} not found", groupName);
        //            return NotFound();
        //        }
        //        return Ok(group);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Failed to retrieve group {GroupName}", groupName);
        //        return StatusCode(500, "Failed to retrieve group");
        //    }
        //}

        //[HttpGet("group-names")]
        //public IActionResult GetGroupNames()
        //{
        //    try
        //    {
        //        List<string> groupNames = _dbService.GetGroupNames().ToList();
        //        _logger.LogInformation($"Retrieved {groupNames.Count} group names");
        //        return Ok(groupNames);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Failed to retrieve group names");
        //        return StatusCode(500, "Failed to retrieve group names");
        //    }
        //}

        //[HttpDelete("{groupName}")]
        //public IActionResult DeleteGroup(string groupName)
        //{
        //    try
        //    {
        //        _dbService.DeleteGroup(groupName);
        //        _logger.LogInformation("Deleted group {GroupName}", groupName);
        //        return StatusCode(204);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Failed to delete group {GroupName}", groupName);
        //        return StatusCode(500, "Failed to delete group");
        //    }
        //}


        //public class CardUpdateModel
        //{
        //    public Card Card { get; set; }
        //    public bool DidSucceed { get; set; }
        //}

        //[HttpPut("update/card")]
        //public IActionResult UpdateCard([FromBody] CardUpdateModel updateModel)
        //{
        //    try
        //    {
        //        var card = _dbService.getCardById(updateModel.Card.Id);
        //        if (card == null)
        //        {
        //            _logger.LogWarning("Card {CardId} not found", updateModel.Card.Id);
        //            return NotFound();
        //        }
        //        _logger.LogInformation("Current attempts: {Attempts}", updateModel.Card.Attempts);
        //        _logger.LogInformation("Current correct attempts: {CorrectAttempts}", updateModel.Card.CorrectAttempts);

        //        _dbService.updateCard(updateModel.Card.Id, updateModel.DidSucceed);

        //        card = _dbService.getCardById(updateModel.Card.Id);
        //        _logger.LogInformation("Current attempts: {Attempts}", updateModel.Card.Attempts);
        //        _logger.LogInformation("Current correct attempts: {CorrectAttempts}", updateModel.Card.CorrectAttempts);

        //        if (card == null)
        //        {
        //            _logger.LogWarning("Card {CardId} not found", updateModel.Card.Id);
        //            return NotFound();
        //        }
        //        _logger.LogInformation("Updated card {CardId}", updateModel.Card.Id);
        //        return StatusCode(204);  // No Content
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Failed to update card {CardId}", updateModel.Card.Id);
        //        return StatusCode(500, "Failed to update card");  // Internal Server Error
        //    }
        //}
    }
}
