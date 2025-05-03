using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servies.Abstractions;
using Shared;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services , IConfiguration configuration)
        {
           services.AddAutoMapper(typeof(AssemblyReference_Service).Assembly);
         services.AddScoped<IServiceManger, ServiceManeger>();
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

            return services;

        }
    }
}
