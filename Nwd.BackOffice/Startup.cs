using BuildingBlock.Core.DomainEvents;
using BuildingBlock.Core.DomainEvents.Events;
using BuildingBlock.EventBus.Abstractions;
using BuildingBlock.EventBus.Rabbitmq;
using BuildingBlock.IntegrationEventLog;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nwd.BackOffice.DI;
using Nwd.BackOffice.Middlewares;
using Nwd.BackOffice.Runtimes;
using Nwd.BackOffice.SalesContext.Commands.Handlers;
using Nwd.BackOffice.SalesContext.Data.Contexts;
using Nwd.BackOffice.SalesContext.Data.Repositories;
using Nwd.BackOffice.SalesContext.Events;
using Nwd.BackOffice.SalesContext.Repositories;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Nwd.BackOffice
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

            // Add DbContext
            services.AddDbContextPool<IntegrationEventLogContext>(options =>
                options.UseSqlServer(Runtime.ConnectionString));

            services.AddDbContextPool<SalesDbContext>(options =>
                options.UseSqlServer(Runtime.ConnectionString));

            // Add Configuration
            services.AddSingleton<IConfiguration>(Configuration);

            // Add RabittMq Settings
            services.Configure<RabbitMqSettings>(options => Configuration.GetSection("RabbitMqSettings").Bind(options));
            services.AddSingleton(service =>
                Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var settings = service.GetService<IOptions<RabbitMqSettings>>().Value;
                    cfg.Host(new Uri(settings.RabbitMqUri), host =>
                    {
                        host.Username(settings.UserName);
                        host.Password(settings.Password);
                    });
                })
            );


            services.AddSingleton(service =>
            {
                var settings = service.GetService<IOptions<RabbitMqSettings>>().Value;
                return new RabbitMqSettings()
                {
                    Exchange = settings.Exchange,
                    RabbitMqUri = settings.RabbitMqUri
                };
            });

            services.AddSingleton<RabbitMqConnection>();
            services.AddSingleton<IEventBus, RabbitMqManager>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<CategoryHandler>();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ProductHandler>();
            services.AddTransient<IDomainEventHandler<ProductApprovedEvent>, ProductEventHandler>();

            DomainEvents.Container = new DomainEventContainer(services.BuildServiceProvider());
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
