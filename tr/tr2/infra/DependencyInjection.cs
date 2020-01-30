using AutoMapper.Configuration;
using domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.AddDbContext<NorthwindContext>
                (options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));

            services.AddScoped<INorthwindContext>(provider => provider.GetService<NorthwindContext>());

            return services;
        }
    }
}
