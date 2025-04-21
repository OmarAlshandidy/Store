using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Data.Context;
using Services;
using Servies.Abstractions;
using Persistance.UnitOfWorks;


namespace Persistance
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
          services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

           services.AddScoped<IDbInitializer, DbInitializer>();  // Allow DI DbInitializer
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            return services;
        } 
        
    }
}
