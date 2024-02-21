namespace Core.Models.Entities
{
    public class MatchOdds : Base
    {
        public Match Match { get; set; }
        public long MatchId { get; set; }
        public string Specifier { get; set; }
        public float Odd { get; set; }
    }
}
