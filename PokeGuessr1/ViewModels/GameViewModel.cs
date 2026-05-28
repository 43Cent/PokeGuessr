namespace PokeGuessr1.Models
{
    public class GameViewModel
    {
        public List<GuessResult> PreviousGuesses { get; set; }
            = new List<GuessResult>();
        public List<string> PokemonNames { get; set; }
            = new List<string>();
        public string GuessInput { get; set; }
        public bool GameWon { get; set; }
        public int RemainingGuesses { get; set; }
        public bool GameLost { get; set; }
        public string CorrectPokemon { get; set; }


    }

}