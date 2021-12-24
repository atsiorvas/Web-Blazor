using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlazorApp.Infrastructure.Data {

    public class AppDbContextSeed {
        public static async Task SeedAsync(AppDbContext catalogContext,
            ILogger logger,
            int retry = 0) {
            var retryForAvailability = retry;
            try {
                if (catalogContext.Database.IsSqlServer()) {
                    catalogContext.Database.Migrate();
                }
            } catch (Exception ex) {
                if (retryForAvailability >= 10)
                    throw;

                retryForAvailability++;

                logger.LogError(ex.Message);
                await SeedAsync(catalogContext, logger, retryForAvailability);
                throw;
            }
        }
    }
}