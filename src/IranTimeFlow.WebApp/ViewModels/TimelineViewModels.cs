using System;
using System.Collections.Generic;

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
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTimeOffset RisedOn { get; set; }

        public string Tags { get; set; }

        public bool Approved { get; set; }

        public string Resources { get; set; }
    }
}
