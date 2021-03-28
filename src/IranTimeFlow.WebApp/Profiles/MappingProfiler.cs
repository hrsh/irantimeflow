using AutoMapper;
using DNTPersianUtils.Core;
using IranTimeFlow.WebApp.Models;
using IranTimeFlow.WebApp.ViewModels;
using System;
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
                        .Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()));

            CreateMap<TimelineAddViewModel, TimelineEntity>()
                .ForMember(
                    a => a.Year,
                    m => m.MapFrom(g => g.RisedOn
                        .ToPersianYearMonthDay(DateTimeOffsetPart.IranLocalDateTime)
                            .Year))
                .ForMember(
                    a => a.Month,
                    m => m.MapFrom(g => g.RisedOn
                        .ToPersianYearMonthDay(DateTimeOffsetPart.IranLocalDateTime)
                            .Month))
                .ForMember(
                    a => a.UniqueId,
                    m => m.MapFrom(g => Guid.NewGuid().ToString("D")))
                .ForMember(
                    a => a.Title,
                    m => m.MapFrom(g => g.Title.ToPersianNumbers()))
                .ForMember(
                    a => a.Content,
                    m => m.MapFrom(g => g.Content.ToPersianNumbers()));
        }
    }
}
