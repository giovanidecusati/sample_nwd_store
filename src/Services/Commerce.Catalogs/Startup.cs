using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nest;
using Commerce.Catalogs.IntegrationEventHandlers;
using Commerce.Catalogs.Repositories;
using Commerce.Catalogs.Middlewares;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;
using System.Diagnostics;

namespace Commerce.Catalogs
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

            // Register ElasticSeach  Nest
            services.AddSingleton(service =>
            {
                var node = new Uri(Configuration.GetSection("ElasticsearchSettings").Value);
                var esSettings = new ConnectionSettings(node);
                esSettings.MapIndexes();
                esSettings.OnRequestCompleted(details => Debug.WriteLine(details.DebugInformation));
                var client = new ElasticClient(esSettings);
                return client;
            });

            // Repositories
            services.AddTransient<IProductRepository, ProductRepository>();

            // Add MassTRansit
            services.AddMassTransit(c =>
            {
                c.AddConsumer<ProductIntegrationEventHandler>();
            });

            services.AddScoped<ProductIntegrationEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider,
            IApplicationLifetime applicationLifetime)
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

            // Configure MassTRansit
            var rmqSettings = Configuration.GetSection("RabbitMqSettings");
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.ExchangeType = ExchangeType.Direct;
                cfg.Durable = true;
                cfg.AutoDelete = false;
                var host = cfg.Host(new Uri(rmqSettings.GetValue<string>("RabbitMqUri")), config =>
                {
                    config.Username(rmqSettings.GetValue<string>("UserName"));
                    config.Password(rmqSettings.GetValue<string>("Password"));
                });

                cfg.ReceiveEndpoint(host, e =>
                {
                    e.LoadFrom(serviceProvider);
                });
            });

            applicationLifetime.ApplicationStarted.Register(bus.Start);
            applicationLifetime.ApplicationStopped.Register(bus.Stop);
        }
    }
}
