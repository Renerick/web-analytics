using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using ProxyKit;
using WebAnalytics.Abstraction;
using WebAnalytics.HeatMaps;
using WebAnalytics.Store.Postgres;
using WebAnalytics.Tracking.Formatters;

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

            services.AddScoped<IInitializer, PostgresInitializer>();
            services.AddScoped<IEventWriter, PostgresStore>();
            services.AddScoped<IRecordingWriter, PostgresStore>();
            services.AddScoped<IAnalyticsStore, PostgresStore>();
            services.AddScoped<IInputFormatter, TextPlainInputFormatter>();

            services.AddSingleton<IHeatmapDrawer, HeatmapDrawer>();

            services.AddCors(opt =>
            {
                opt.AddDefaultPolicy(
                    builder =>
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .SetIsOriginAllowedToAllowWildcardSubdomains());
            });

            services.AddProxy();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../WebAnalytics.WebApp/dist";
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
                var proxyApp = endpoints.CreateApplicationBuilder();

                proxyApp.RunProxy(context =>
                {
                    var url = context.GetRouteValue("site").ToString();

                    context.Request.Path = "";
                    context.Request.QueryString = QueryString.Empty;

                    var response = context.ForwardTo(url)
                                  .Send();

                    response.Result.Headers.Add("Access-Control-Allow-Origin", "http://localhost:8081");

                    return response;
                });


                endpoints.Map("/proxy/{*site}", proxyApp.Build());
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../WebAnalytics.WebApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:8081");
                }
            });
        }
    }
}