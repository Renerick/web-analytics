using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using WebAnalytics.Abstraction;
using WebAnalytics.Abstraction.Db;
using WebAnalytics.Db.Postgres;
using WebAnalytics.Db.SimpleWriter;

namespace WebAnalytics.Tracking
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDbConnection>(_ => new NpgsqlConnection(Configuration.GetConnectionString("master")));

            services.AddScoped<IEventStore, PostgresEventStore>();
            services.AddScoped<IInitializer, PostgresInitializer>();

            services.AddScoped<IEventWriter, SimpleEventWriter>();

            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(
                    builder =>
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .SetIsOriginAllowedToAllowWildcardSubdomains());
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IEnumerable<IInitializer> initializers)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            foreach (var initializer in initializers)
            {
                logger.LogInformation($"Initializing {initializer.GetType().Name}");
                initializer.EnsureInitialized();
            }

            app.UseCors();

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}