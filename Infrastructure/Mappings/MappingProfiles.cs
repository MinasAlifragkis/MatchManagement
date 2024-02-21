using AutoMapper;
using Core.Models.DTO;
using Core.Models.Entities;
using System;
using System.Globalization;

namespace Infrastructure.Mappings
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Match, MatchDTO>()
                .ForMember(dest => dest.MatchDate, opt => opt.MapFrom(src => src.MatchDate.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.MatchTime, opt => opt.MapFrom(src => src.MatchTime.ToString("hh\\:mm")))
                .ReverseMap()
                .ForMember(dest => dest.MatchDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.MatchDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)));
            CreateMap<MatchOdds, MatchOddsDTO>()
                .ReverseMap();            
        }
    }
}
