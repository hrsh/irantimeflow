using System;

namespace IranTimeFlow.BackService.Models
{
    public class TimelineEntity
    {
        public int Id { get; set; }

        public string UniqueId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTimeOffset RisedOn { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public string Tags { get; set; }

        public bool Approved { get; set; }

        public bool Published { get; set; }

        public string Resources { get; set; }

        public string CreatedByEmail { get; set; }
    }
}
