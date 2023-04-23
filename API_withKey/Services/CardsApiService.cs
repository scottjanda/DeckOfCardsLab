using DeckOfCardsLab.Models;

namespace DeckOfCardsLab.Services
{
    public class CardsApiService
    {
        // Inject the HttpClient
        private readonly HttpClient _httpClient;
        public CardsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DeckResponse> GetDeck(int deckCount)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count={deckCount}");
            var result = await response.Content.ReadAsAsync<DeckResponse>();
            return result;
        }

        public async Task<DrawResponse> DrawCards(int cardCount, string deck_id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://deckofcardsapi.com/api/deck/{deck_id}/draw/?count={cardCount}");
            var result = await response.Content.ReadAsAsync<DrawResponse>();
            return result;
        }

        public async Task<PlayerHand> GetPile(string deck_id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://deckofcardsapi.com/api/deck/{deck_id}/pile/player1/list/");
            var result = await response.Content.ReadAsAsync<PlayerHand>();
            return result;
        }

        public async Task<PlayerHand> AddCards(string deck_id, string cardsAdded)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://deckofcardsapi.com/api/deck/{deck_id}/pile/player1/add/?cards={cardsAdded}");
            var result = await response.Content.ReadAsAsync<PlayerHand>();
            return result;
        }
   
        public async Task<PlayerHand> RemoveCards(int cardsRemoved, string deck_id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://deckofcardsapi.com/api/deck/{deck_id}/pile/player1/draw/bottom/?count={cardsRemoved}");
            var result = await response.Content.ReadAsAsync<PlayerHand>();
            return result;
        }
    }
}
