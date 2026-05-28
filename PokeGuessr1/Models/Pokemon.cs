namespace PokeGuessr1.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Generation { get; set; }
        public string Type1 { get; set; } = string.Empty;
        public string Type2 { get; set; } = string.Empty;
        public double Weight { get; set; }
    }
}