using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IranTimeFlow.WebApp.Models
{
    public class TimelineEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTimeOffset RisedOn { get; set; }

        public int Year { get; set; }

        public int Month { get; set; }

        public string Tags { get; set; }
    }

    public class TimelineEntityConfig : IEntityTypeConfiguration<TimelineEntity>
    {
        public void Configure(EntityTypeBuilder<TimelineEntity> builder)
        {
            builder.Property(a => a.Title).IsRequired().HasMaxLength(256);
            builder.Property(a => a.Content).IsRequired();
            builder.Property(a => a.Tags).IsRequired().HasMaxLength(1024);
        }
    }
}
