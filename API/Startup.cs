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
using Services.Interfaces.Services;
using Services.Services;
using System;
using System.IO;
using System.Reflection;

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
    }
}
