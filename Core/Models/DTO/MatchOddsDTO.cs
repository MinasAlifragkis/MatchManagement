namespace Core.Models.DTO
{
    public class MatchOddsDTO : BaseDTO
    {
        public long MatchId { get; set; }
        public string Specifier { get; set; }
        public float Odd { get; set; }
    }
}
