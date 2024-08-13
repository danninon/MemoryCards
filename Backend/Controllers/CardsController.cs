using Microsoft.AspNetCore.Mvc;
using Backend.Database.Models;
using Backend.Business.Repositories;


namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ILogger<CardsController> _logger;
        private readonly ICardRepository _cardRepository;

        public CardsController(ICardRepository dbService, ILogger<CardsController> logger)
        {
            _logger = logger;
            _cardRepository = dbService;
        }

        [HttpPut("{cardId}/update-attempts")]
        public async Task<IActionResult> UpdateCardAttempts(string cardId, [FromBody] bool succeeded)
        {
            _logger.LogInformation($"Received request to update attempts for card {cardId}. Success: {succeeded}");

            try
            {
                // Fetch the card by ID
                var card = await _cardRepository.GetCardByIdAsync(cardId);
                if (card == null)
                {
                    _logger.LogWarning($"Card with ID {cardId} not found.");
                    return NotFound("Card not found.");
                }

                // Update the attempts
                card.TotalAttempts += 1;
                if (succeeded)
                {
                    card.CorrectAttempts += 1;
                }

                // Update the LastUpdated field
            

                // Save the updated card back to the repository
                await _cardRepository.UpdateCardAsync(card);
                _logger.LogInformation($"Card {cardId} updated successfully. TotalAttempts: {card.TotalAttempts}, CorrectAttempts: {card.CorrectAttempts}");

                return Ok(new { Message = "Card attempts updated successfully", Card = card });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating card {cardId}.");
                return StatusCode(500, "An error occurred while updating the card.");
            }
        }
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
