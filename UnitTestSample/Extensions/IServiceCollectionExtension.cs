using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UnitTestSample.IServices;
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

            services.AddDbContext<BloggingContext>();
            return services;
        }
    }
}