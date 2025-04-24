using Microsoft.EntityFrameworkCore;
using Readify.DAL.DBContext;

namespace Readify.API.Helpers
{

    public class ApplySeeding
    {
        // perform seeding tasks during the application startup
        // WebApplication app => class, which represents the running web application
        public static async Task ApplySeedingAsync(WebApplication app)
        {
            // lazem a2wimhom m3aia 

            // This line starts a using block that creates a new scope for dependency injection.
            // Scopes are used to ensure that services are created and disposed of properly
            using (var scope = app.Services.CreateScope())
            {
                // gives access to all the registered services within the application's dependency injection container
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = service.GetRequiredService<ApplicationDBContext>();

                    await context.Database.MigrateAsync();

                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<ApplicationDBContext>();
                    logger.LogError(ex.Message);
                }

            }
        }
    }
}
