using IranTimeFlow.BackService.Models;
using Microsoft.EntityFrameworkCore;

namespace IranTimeFlow.BackService.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TimelineEntity> Timelines { get; set; }
    }
}
