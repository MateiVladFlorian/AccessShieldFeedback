using Microsoft.EntityFrameworkCore;

namespace AccessShieldFeedback.Data
{
    public class FeedbackContext : DbContext
    {
        public FeedbackContext(DbContextOptions<FeedbackContext> options) : base(options)
        { }

        public DbSet<Feedback> Feedback { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* binds the entity table to existing database */
            modelBuilder.Entity<Feedback>().ToTable(nameof(Feedback));
        }
    }
}
