using System.Globalization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using PokeGuessr1.Models;

namespace PokeGuessr1.Services
{
    public class GameService
    {
        private readonly IWebHostEnvironment _env;

        public GameService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public List<Pokemon> LoadPokemon()
        {
            var pokemonList = new List<Pokemon>();
            var path = Path.Combine(_env.WebRootPath, "Pokedex.csv");

            if (!File.Exists(path))
                throw new FileNotFoundException("Pokedex.csv not found in web root.", path);

            foreach (var line in File.ReadLines(path).Skip(1))
            {
                var values = line.Split(',');

                if (values.Length < 6) continue;

                pokemonList.Add(new Pokemon
                {
                    Name = values[0],
                    Id = int.Parse(values[1], CultureInfo.InvariantCulture),
                    Generation = int.Parse(values[2], CultureInfo.InvariantCulture),
                    Type1 = values[3],
                    Type2 = values[4],
                    Weight = double.Parse(values[5], CultureInfo.InvariantCulture)
                });
            }

            return pokemonList;
        }

        public Pokemon GetRandomPokemon(List<Pokemon> pokemonList)
        {
            Random random = new Random();

            return pokemonList[random.Next(pokemonList.Count)];
        }
    }
}