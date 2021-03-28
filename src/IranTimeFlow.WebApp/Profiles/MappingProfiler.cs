using AutoMapper;
using DNTPersianUtils.Core;
using IranTimeFlow.WebApp.Models;
using IranTimeFlow.WebApp.ViewModels;
using System.Linq;

namespace IranTimeFlow.WebApp.Profiles
{
    public class MappingProfiler : Profile
    {
        public MappingProfiler()
        {
            CreateMap<TimelineEntity, TimelineViewModel>()
                .ForMember(
                    a => a.DateRised,
                    m => m.MapFrom(g => g.RisedOn
                        .ToPersianDateTextify(DateTimeOffsetPart.IranLocalDateTime)
                        .ToPersianNumbers()))
                .ForMember(
                    a => a.Tagline,
                    m => m.MapFrom(g => g.Tags
                        .Split(',', System.StringSplitOptions.RemoveEmptyEntries).ToList()));
        }
    }
}
