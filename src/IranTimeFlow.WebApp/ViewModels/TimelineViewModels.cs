using IranTimeFlow.WebApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IranTimeFlow.WebApp.ViewModels
{
    public class TimelineViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTimeOffset RisedOn { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public List<string> Tagline { get; set; }

        public bool Approved { get; set; }

        public List<string> Resources { get; set; }

        public string DateRised { get; set; }
    }

    public class TimelineListViewModel
    {
        public IEnumerable<TimelineViewModel> Timelines { get; set; }
    }

    public class TimelineAddViewModel
    {
        [Display(Name = "عنوان خبر")]
        [Required(ErrorMessage = Consts.DefaultRequired)]
        [StringLength(128, ErrorMessage = Consts.DefaultLengthRange, MinimumLength = 8)]
        public string Title { get; set; }

        [Display(Name = "متن و محتوای خبر")]
        [Required(ErrorMessage = Consts.DefaultRequired)]
        [StringLength(4096, ErrorMessage = Consts.DefaultLengthRange, MinimumLength = 32)]
        public string Content { get; set; }

        public DateTimeOffset RisedOn { get; set; }

        [Display(Name = "برچسب‌ها")]
        [StringLength(1024, ErrorMessage = Consts.DefaultLengthRange, MinimumLength = 4)]
        public string Tags { get; set; }

        public bool Approved { get; set; }

        public bool Published { get; set; }

        [Display(Name = "لینک منابع")]
        [Required(ErrorMessage = Consts.DefaultRequired)]
        [StringLength(4096, ErrorMessage = Consts.DefaultLengthRange, MinimumLength = 8)]
        public string Resources { get; set; }
    }
}
