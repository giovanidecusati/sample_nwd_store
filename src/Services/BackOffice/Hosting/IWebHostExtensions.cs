﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.AspNetCore.Hosting
{
    public static class IWebHostExtensions
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext, ILogger<TContext>> seeder) where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();
                var context = scope.ServiceProvider.GetService<TContext>();

                try
                {
                    context.Database.Migrate();

                    seeder(context, logger);
                }
                catch (Exception ex)
                {                    
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            return webHost;
        }
    }
}