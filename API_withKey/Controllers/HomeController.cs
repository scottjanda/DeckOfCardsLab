using DeckOfCardsLab.Models;
using DeckOfCardsLab.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using static DeckOfCardsLab.Models.DrawResponse;

namespace DeckOfCardsLab.Controllers
{
    public class HomeController : Controller
    {

        // Use dependency injection to get the Animal Api Service
        private readonly CardsApiService _cardsApiService;
        public HomeController(CardsApiService cardsApiService)
        {
            _cardsApiService = cardsApiService;
        }

        public async Task<IActionResult> Index(string deck_id = null)
        {
            try
            {
                if (deck_id is null)
                {
                    var deckResult = await _cardsApiService.GetDeck(1);
                    deck_id = deckResult.deck_id;
                }

                var cardResult = await _cardsApiService.DrawCards(5, deck_id);

                string cardsToAdd = "";
                foreach (var card in cardResult.cards)
                {
                    cardsToAdd += (card.code) + ",";
                }
                string cardsToAddFormatted = cardsToAdd.Remove(cardsToAdd.Length - 1,1);
                var addCards = await _cardsApiService.AddCards(deck_id, cardsToAddFormatted);

                if (cardResult.remaining < 47)
                {
                    await _cardsApiService.RemoveCards(5, deck_id);
                }

                var pileResult = await _cardsApiService.GetPile(deck_id);
                return View(pileResult);

            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                Debug.WriteLine(ex);
                return View();
            }
        }

    }
}