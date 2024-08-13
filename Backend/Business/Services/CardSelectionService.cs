using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Business.Repositories;
using Backend.Database.Models;


namespace Backend.Services
{
    public class CardSelectionService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IGroupRepository _groupRepository;

        public CardSelectionService(ICardRepository cardRepository, IGroupRepository groupRepository)
        {
            _cardRepository = cardRepository;
            _groupRepository = groupRepository;
        }

        public async Task<List<Card>> SelectCardsAsync(List<string> groupIds, int numberOfCards)
        {
            var allCards = new List<Card>();

            // Step 1: Collect all cards from the selected groups
            foreach (var groupId in groupIds)
            {
                var group = await _groupRepository.GetGroupByIdAsync(groupId);
                if (group == null) continue;

                var cards = await _cardRepository.GetCardsByGroupIdAsync(groupId);
                allCards.AddRange(cards);
            }

            // Step 2: Pick half of the cards based on the highest failure rate
            int halfNumberOfCards = numberOfCards / 2;
            var mostFailedCards = allCards
                .Where(c => c.TotalAttempts > 0)
                .OrderByDescending(c => (double)(c.TotalAttempts - c.CorrectAttempts) / c.TotalAttempts)
                .Take(halfNumberOfCards)
                .ToList();

            // Step 3: Pick the remaining cards based on the longest time since last update
            var remainingCards = allCards.Except(mostFailedCards).ToList(); // Exclude already selected cards
            var oldestCards = remainingCards
                .OrderBy(c => c.LastUpdated)
                .Take(numberOfCards - mostFailedCards.Count)
                .ToList();

            // Step 4: Combine the selected cards
            var selectedCards = mostFailedCards.Concat(oldestCards).ToList();

            // Ensure the final list contains exactly `numberOfCards` items
            if (selectedCards.Count > numberOfCards)
            {
                selectedCards = selectedCards.Take(numberOfCards).ToList();
            }

            // Step 5: Shuffle the selected cards to randomize the order
            var random = new Random();
            selectedCards = selectedCards.OrderBy(c => random.Next()).ToList();

            return selectedCards;
        }
    }
}
