using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Entities
{
    public class Match : Base
    {
        public enum SportType
        {
            Football = 1,
            Basketball = 2
        }
        public string Description { get; set; }
        [Column(TypeName = "date")]
        public DateTime MatchDate { get; set; }
        [Column(TypeName = "time")]
        public TimeSpan MatchTime { get; set; }
        public string TeamA { get; set; }
        public string TeamB { get; set; }
        public SportType Sport { get; set; }
        public ICollection<MatchOdds> MatchOdds { get; set; }
    }
}
