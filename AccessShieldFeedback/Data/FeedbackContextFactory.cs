using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AccessShieldFeedback.Data
{
    public class FeedbackContextFactory : IDesignTimeDbContextFactory<FeedbackContext>
    {
        /* Required fix at Entity Framework to successful create database migrations. */
        public FeedbackContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var builderContext = new DbContextOptionsBuilder<FeedbackContext>();
            var connectionString = configurationRoot.GetConnectionString("AccessControlFeedback");

            builderContext.UseNpgsql(connectionString);
            return new FeedbackContext(builderContext.Options);
        }

    }
}
