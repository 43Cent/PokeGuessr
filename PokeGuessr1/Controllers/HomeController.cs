using Microsoft.AspNetCore.Mvc;
using PokeGuessr1.Models;
using PokeGuessr1.Services;

namespace PokeGuessr1.Controllers
{
    public class HomeController : Controller
    {
        private readonly GameService _gameService;

        public HomeController(GameService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            var pokemonList = _gameService.LoadPokemon();

            var answer = _gameService.GetRandomPokemon(pokemonList);

            ViewBag.Answer = answer.Name;

            return View(pokemonList);
        }
    }
}