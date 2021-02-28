using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using UnitTestSample.Appsettings;
using UnitTestSample.Interfaces;
using UnitTestSample.Services;

namespace UnitTestSample.Extensions
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

            return services;
        }

        public static IServiceCollection AddMyConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AzureMaps>(configuration.GetSection(nameof(AzureMaps)));

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "UnitTestSample",
                    Version = "v1"
                });
            });
        }
    }
}