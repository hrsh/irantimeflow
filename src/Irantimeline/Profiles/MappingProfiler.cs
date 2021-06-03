using AutoMapper;
using DNTPersianUtils.Core;
using DNTPersianUtils.Core.Normalizer;
using Irantimeline.Models;
using System;
using System.Linq;

namespace Irantimeline.Profiles
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
                        .Split('-', StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(
                    a => a.Resources,
                    m => m.MapFrom(g => g.Resources
                        .Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()));

            CreateMap<TimelineAddViewModel, TimelineEntity>()
                .ForMember(
                    a => a.Year,
                    m => m.MapFrom(g => g.RisedOn
                        .ToPersianYearMonthDay(DateTimeOffsetPart.IranLocalDateTime).Year))
                .ForMember(
                    a => a.Month,
                    m => m.MapFrom(g => g.RisedOn
                        .ToPersianYearMonthDay(DateTimeOffsetPart.IranLocalDateTime).Month))
                .ForMember(
                    a => a.UniqueId,
                    m => m.MapFrom(g => Guid.NewGuid().ToString("D")))
                .ForMember(
                    a => a.Title,
                    m => m.MapFrom(g => g.Title.ToPersianNumbers().ApplyCorrectYeKe().ApplyHalfSpaceRule()))
                .ForMember(
                    a => a.Content,
                    m => m.MapFrom(g => g.Content.ToPersianNumbers().ApplyCorrectYeKe().ApplyHalfSpaceRule()));

            CreateMap<TimelineEntity, TimelineEditViewModel>();

            CreateMap<TimelineEditViewModel, TimelineEntity>()
                //.ForMember(
                //    a => a.Year,
                //    m => m.MapFrom(g => g.RisedOn
                //        .ToPersianYearMonthDay(DateTimeOffsetPart.IranLocalDateTime).Year))
                //.ForMember(
                //    a => a.Month,
                //    m => m.MapFrom(g => g.RisedOn
                //        .ToPersianYearMonthDay(DateTimeOffsetPart.IranLocalDateTime).Month))
                .ForMember(
                    a => a.Title,
                    m => m.MapFrom(g => g.Title.ToPersianNumbers().ApplyCorrectYeKe().ApplyHalfSpaceRule()))
                .ForMember(
                    a => a.Content,
                    m => m.MapFrom(g => g.Content.ToPersianNumbers().ApplyCorrectYeKe().ApplyHalfSpaceRule()));
        }
    }
}
