using Microsoft.AspNetCore.Mvc;
using PokeGuessr1.Models;
using PokeGuessr1.Services;
using System;
using System.Text.Json;
using System.Text.Json;


namespace PokeGuessr1.Controllers
{
    public class HomeController : Controller
    {
        private readonly GameService _gameService;

        public HomeController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var pokemonList = _gameService.LoadPokemon();

            var model = new GameViewModel();
            model.PreviousGuesses = new List<GuessResult>();
            model.RemainingGuesses = 10 - model.PreviousGuesses.Count;

            if (HttpContext.Session.GetString("Answer") == null)
            {
                var answer = _gameService.GetRandomPokemon(pokemonList);

                HttpContext.Session.SetString("Answer", answer.Name);
            }

            model.PokemonNames = pokemonList.Select(p => p.Name).ToList();

            return View(model);
        }

       
        [HttpPost]
        public IActionResult Index(GameViewModel model)
        {
            var pokemonList = _gameService.LoadPokemon();
            model.RemainingGuesses = 10 - model.PreviousGuesses.Count;

            string answerName =
                HttpContext.Session.GetString("Answer");

            var answer = pokemonList.FirstOrDefault
                (p => p.Name == answerName);

            var guess = pokemonList.FirstOrDefault
                (p => p.Name.ToLower()
                == model.GuessInput.ToLower());

            // Load previous guesses from session
            var savedGuesses =
                HttpContext.Session.GetString("Guesses");

            List<GuessResult> previousGuesses;

            if (savedGuesses == null)
            {
                previousGuesses = new List<GuessResult>();
            }
            else
            {
                previousGuesses =
                    JsonSerializer.Deserialize<List<GuessResult>>
                    (savedGuesses);
            }

            if (guess != null)
            {
                var result = new GuessResult
                {
                    Guess = guess,

                    NameCorrect = guess.Name == answer.Name,

                    
                    GenerationHint = guess.Generation == answer.Generation
                            ? "✅"
                            : guess.Generation < answer.Generation
                                ? "⬆"
                                : "⬇",

                    
                    Type1Result = guess.Type1 == answer.Type1? "green"
                   : guess.Type1 == answer.Type2
                                ? "yellow"
                                : "red",

                   Type2Result =guess.Type2 == answer.Type2? "green"
                    : guess.Type2 == answer.Type1
                        ? "yellow"
                        : "red",


                  
                    WeightHint = guess.Weight == answer.Weight
                        ? "✅"
                        : guess.Weight < answer.Weight
                            ? "⬆"
                            : "⬇"
                };

                previousGuesses.Add(result);

                
                if (previousGuesses.Count >= 10 && !result.NameCorrect)
                {
                    model.GameLost = true;

                    model.CorrectPokemon = answer.Name;
                }


                if (result.NameCorrect)
                {
                    model.GameWon = true;
                }
            }

            // Save guesses back into session
            HttpContext.Session.SetString(
                "Guesses",
                JsonSerializer.Serialize(previousGuesses)
            );

            model.PreviousGuesses = previousGuesses;
            model.RemainingGuesses = 10 - previousGuesses.Count;
            model.PokemonNames = pokemonList.Select(p => p.Name).ToList();

            return View(model);
        }

        [HttpPost]
        public IActionResult Reset()
        {
            HttpContext.Session.Remove("Answer");

            HttpContext.Session.Remove("Guesses");

            return RedirectToAction("Index");
        }

    }
}
