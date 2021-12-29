using System;
using System.Threading.Tasks;
using BlazorApp.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlazorApp.PublicApi {
    public class Program {
        public static async Task Main(string[] args) {
            var builder = CreateHostBuilder(args).Build();

            using var scope = builder.Services.CreateScope();
            var scopedProvider = scope.ServiceProvider;
            ILogger logger = null;
            try {
                var catalogContext = scopedProvider.GetRequiredService<AppDbContext>();
                logger = scopedProvider.GetRequiredService<ILogger<Program>>();
                await AppDbContextSeed.SeedAsync(catalogContext, logger);

            } catch (Exception ex) {
                logger?.LogError(ex, "An error occurred seeding the DB.");
            }

            await builder.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}