using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Servies.Abstractions;

namespace Services
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
           services.AddAutoMapper(typeof(AssemblyReference_Service).Assembly);
         services.AddScoped<IServiceManger, ServiceManeger>();
            return services;

        }
    }
}
