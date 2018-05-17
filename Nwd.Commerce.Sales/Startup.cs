using BuildingBlock.EventBus.Abstractions;
using BuildingBlock.EventBus.Rabbitmq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;
using Nwd.Commerce.CatalogsContext.IntegrationEventHandlers;
using Nwd.Commerce.CatalogsContext.Repositories;
using Nwd.Commerce.Middlewares;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace Nwd.Commerce
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
                c.SwaggerDoc("v1", new Info { Title = "Northwind Commerce Web API", Version = "v1" });
            });

            // Add Configuration
            services.AddSingleton<IConfiguration>(Configuration);

            // Repositories
            services.AddTransient<IProductRepository, ProductRepository>();

            // Register ElasticSeach  Nest
            services.AddSingleton(service =>
            {
                var node = new Uri(Configuration.GetSection("ElasticsearchSettings").Value);
                var settings = new ConnectionSettings(node);
                var client = new ElasticClient(settings);
                return client;
            });

            // Register RabittMq Settings
            services.Configure<RabbitMqSettings>(options => Configuration.GetSection("RabbitMqSettings").Bind(options));
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

            services.AddSingleton<ProductIntegrationEventHandler>();

            var provider = services.BuildServiceProvider();
            var bus = provider.GetService<IEventBus>();
            bus.Subscribe(provider.GetService<ProductIntegrationEventHandler>());

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
