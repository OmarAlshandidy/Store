using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistance;
using Persistance.Identity;
using Services;
using Shared;
using Shared.ErrorModels;
using Store.G04.Api.Middlewares;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Store.G04.Api.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection RegisterAllServieces(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddBuiltInServies();
            services.AddSwaggerServices();

            services.AddIdentityServies();
          services.AddInfrastructureServices(configuration);
          services.AddApplicationServices(configuration);
           

            services.ConfigureServies();

            services.ConfigureJwtService(configuration);


            return services;
        }
        private static IServiceCollection AddBuiltInServies(this IServiceCollection services)
        {
            services.AddControllers();
            return services;

        }
        private static IServiceCollection ConfigureJwtService (this IServiceCollection services ,IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.AddAuthentication(opation =>
            {
                opation.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opation.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))

                };
            });
            return services;

        }

        private static IServiceCollection AddIdentityServies(this IServiceCollection services)
        {
            services.AddIdentity<AppUser,IdentityRole>()
              .AddEntityFrameworkStores<StoreIdentityDbContext>();
            return services;

        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;

        }
        private static IServiceCollection ConfigureServies(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                                 .Select(m => new ValidationError()
                                                 {
                                                     Field = m.Key,
                                                     Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)
                                                 });

                    var response = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);

                };
            });
            return services;

        }

        public static async Task<WebApplication> ConfigureMiddleWares(this WebApplication app)
        {

            await app.InitializeDatabaseAsync();

             app.UseGlobalErrorHandling();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            return app;
        }

        private static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
        {


            #region Seeding
            using var scope = app.Services.CreateScope();
            var dbIntializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); // Ask ClR CReate Object From DbInitializer
            await dbIntializer.InitializeAsync();
            await dbIntializer.IdintityInitializeAsync();


            #endregion
            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {


            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return app;
        }
    }
       
}
