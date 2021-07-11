using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Http;
using Microsoft.OpenApi.Models;
using System.Net.Http;
using WebApiSample.Appsettings;
using WebApiSample.Interfaces;
using WebApiSample.Services;

namespace WebApiSample.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddMySettings(this IServiceCollection services)
        {
            services.AddTransient<IDatetimeService, DatetimeService>();
            services.AddTransient<IHeavyWorkOne, HeavyWorkOne>();
            services.AddTransient<IHeavyWorkTwo, HeavyWorkTwo>();
            services.AddTransient<IWeatherForecastService, WeatherForecastService>();
            services.AddTransient<IAzureMapsClientService, AzureMapsClientService>();
            services.AddDbContext<BloggingContext>();
            services.AddHttpClient();
            return services;
        }

        public static IServiceCollection AddMyConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AzureMaps>(configuration.GetSection(nameof(AzureMaps)));
            services.AddHealthChecks()
                    .AddCheck(
                        "Sample",
                        new SampleHealthCheckService(),
                        HealthStatus.Unhealthy,
                        new string[] { "Sample" });
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "WebApiSample",
                    Version = "v1"
                });
            });
        }
    }
}