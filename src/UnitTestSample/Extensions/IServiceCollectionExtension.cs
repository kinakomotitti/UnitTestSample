﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UnitTestSample.Interfaces;
using UnitTestSample.Services;

namespace UnitTestSample.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddMySettings(this IServiceCollection services)
        {
            services.AddTransient<IDatetimeService, DatetimeService>();
            services.AddTransient<HeavyWorkOne, HeavyWorkOne>();
            services.AddTransient<IHeavyWorkTwo, HeavyWorkTwo>();

            services.AddDbContext<BloggingContext>();
            return services;
        }
    }
}