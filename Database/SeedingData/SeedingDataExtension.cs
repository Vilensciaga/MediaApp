using Database.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Database.SeedingData
{
    public static class SeedingDataExtension
    {
        /*
         * using method to create the database from migrations on file
         * call Seed static method in database/seedindData 
         * logs error on failure
         */
        public static async Task SeedDatabase(this IHost app)
        {
            await using (var scope = app.Services.CreateAsyncScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<IAppDbContext>();
                    //applies pending migrations or create database if it dont exixt
                    await context.Database.MigrateAsync();
                    await Seed.SeedUsers(context);
                }
                catch (Exception ex)
                {
                    // handle errors or log them
                    //var logger = services.GetRequiredService<ILogger<Program>>();
                    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                    var logger = loggerFactory.CreateLogger("DataSeeding");
                    logger.LogError(ex, "Error during DB seeding or migration");

                }
            }

        }
    }
}
