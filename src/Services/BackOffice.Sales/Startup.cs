using BuildingBlock.IntegrationEventLog;
using BuildingBlock.IntegrationEventLog.Services;
using MassTransit;
using MassTransit.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BackOffice.Sales.Middlewares;
using BackOffice.Sales.Runtimes;
using BackOffice.Sales.Data.Contexts;
using BackOffice.Sales.Mappings;
using BackOffice.Sales.Services;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Data.Common;

namespace BackOffice.Sales
{
    public class Startup
    {
        private IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                configurationBuilder.AddUserSecrets<Startup>();
            }

            Configuration = configurationBuilder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            bool IsIntegratedTest() => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "IntegrationTest";

            // MVC
            services.AddMvc();

            // Compression
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();

            // Cors
            services.AddCors();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Northwind BackOffice Web API", Version = "v1" });
            });

            Runtime.ConnectionString = Configuration.GetConnectionString("DefaultConnection");

            MapperExtension.RegisterProfiles();

            // Add DbContext
            services.AddDbContextPool<IntegrationEventLogContext>(options =>
                options.UseSqlServer(Runtime.ConnectionString));

            services.AddDbContextPool<SalesDbContext>(options =>
                options.UseSqlServer(Runtime.ConnectionString));

            // Add Configuration
            services.AddSingleton<IConfiguration>(Configuration);

            // Add RabittMq Settings
            services.AddSingleton(service =>
           {
               var settings = Configuration.GetSection("RabbitMqSettings");
               var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
               {
                   cfg.ExchangeType = ExchangeType.Direct;
                   cfg.Durable = true;
                   cfg.AutoDelete = false;
                   cfg.Host(new Uri(settings.GetValue<string>("RabbitMqUri")), host =>
                   {
                       host.Username(settings.GetValue<string>("UserName"));
                       host.Password(settings.GetValue<string>("Password"));
                   });
               });

               TaskUtil.Await(() => bus.StartAsync());

               return bus;
           });

            services.AddTransient<Func<DbConnection, IntegrationEventLogService>>(
               sp => (DbConnection c) => new IntegrationEventLogService(c));
            services.AddTransient<IntegrationEventLogService>();
            services.AddTransient<IProductIntegrationEventService, ProductIntegrationEventService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind Commerce Web API v1");
                c.RoutePrefix = string.Empty;
            });

            // Write Log on Console
            loggerFactory.AddConsole();

            // Apply Compression
            app.UseResponseCompression();

            // Apply Exception Handler
            app.UseExceptionHandler(p => GlobalExceptionHandlerMiddleware.Handle(p, env.IsDevelopment()));

            // Enable Cors * * * 
            app.UseCors(x =>
            {
                x.AllowAnyHeader();
                x.AllowAnyMethod();
                x.AllowAnyOrigin();
            });

            app.UseMvc();
        }
    }
}
