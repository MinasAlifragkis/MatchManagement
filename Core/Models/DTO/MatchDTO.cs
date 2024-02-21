using Core.Attributes;

namespace Core.Models.DTO
{
    public class MatchDTO : BaseDTO
    {
        public enum SportType
        {
            Football = 1,
            Basketball = 2
        }
        public string Description { get; set; }
        [DateFormatValidation(ErrorMessage = "Invalid date format. Date should be in dd/mm/yyyy format.")]
        public string MatchDate { get; set; }
        [TimeFormatValidation(ErrorMessage = "Invalid time format. Time should be in HH:mm format.")]
        public string MatchTime { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public SportType Sport { get; set; }
    }
}
