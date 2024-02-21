using Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchOdds> MatchOdds { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                .HasMany(c => c.MatchOdds)
                .WithOne(c => c.Match)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MatchOdds>()
                .HasOne(c => c.Match)
                .WithMany(c => c.MatchOdds);
        }
    }
}
