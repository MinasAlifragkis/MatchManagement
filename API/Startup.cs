using Core.Interfaces.Data;
using Infrastructure.Mappings;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using Services.Interfaces.Services;
using Services.Services;
using System;
using System.IO;
using System.Reflection;
using Serilog.Exceptions;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Database Configuration
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var appRunsInDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER");
            if (!string.IsNullOrEmpty(appRunsInDocker))
            {
                var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
                var dbName = Environment.GetEnvironmentVariable("DB_NAME");
                var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
                connectionString = $"Server={dbHost},1433;Database={dbName};User Id=sa;Password={dbPassword};TrustServerCertificate=True";
            }
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            services.AddSwaggerGen(options =>
            {
                options.IncludeXmlComments(xmlPath);
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Match Management"
                });
            });

            services.AddAutoMapper(c =>
            {
                c.AddProfile<MappingProfiles>();
            });
            ConfigureScopedServices(services);

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();
            
            ConfigureLogging(configuration, environment);
            ConfigureRedis(services, configuration, environment);
        }

        private void ConfigureScopedServices(IServiceCollection services)
        {
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<IMatchOddsRepository, MatchOddsRepository>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IMatchOddsService, MatchOddsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        private void ConfigureLogging(IConfigurationRoot configuration, string env)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, env))
                .Enrich.WithProperty("Environment", env)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM-dd}",
                NumberOfReplicas = 1,
                NumberOfShards = 2,
            };
        }

        private void ConfigureRedis(IServiceCollection services, IConfigurationRoot configuration, string env)
        {
            var redisConnectionString = configuration["CacheSettings:ConnectionString"];
            bool appRunsInDocker;
            bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out appRunsInDocker);
            if (appRunsInDocker)
                redisConnectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING");
            services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = redisConnectionString;
                });
        }
    }
}
