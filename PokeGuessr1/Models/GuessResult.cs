using PokeGuessr1.Models;

namespace PokeGuessr1.Models{
    public class GuessResult{
        public Pokemon Guess { get; set; }
        public bool NameCorrect { get; set; }
        public string GenerationHint { get; set; }
        public string Type1Result { get; set; }
        public string Type2Result { get; set; }
        public string WeightHint { get; set; }
    }
}