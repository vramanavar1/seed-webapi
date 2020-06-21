using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using PersistenceLayer.Contracts;
using PersistenceLayer.Repositories;
using SeedWebApi.Filters;
using ServiceLayer;
using ServiceLayer.Contracts;
using System;

namespace SeedWebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.Development.json", optional: true)
            .AddJsonFile($"appsettings.Production.json", optional: true)
            .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase(connectionString), ServiceLifetime.Scoped);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IDiamondService, DiamondService>();

            services.AddHealthChecks();

            services.AddMvc(config =>
            {
                // Add XML Content Negotiation
                config.RespectBrowserAcceptHeader = true;

                // Add Global Error Handling Filter
                config.Filters.Add(typeof(GlobalExceptionFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(opts =>
            {
                // Force  Camelcase to JSON serialization
                opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }).AddControllersAsServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DatabaseContext databaseContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /* 
             * NOTE: Here's a very basic health check. This can be further extended to validate the health of downstream services
             * and provide sophisticated health status report based on custom
             */ 
            app.UseHealthChecks("/_healthz");

            app.UseMvc();
        }
    }
}
