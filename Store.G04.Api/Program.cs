
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistance;
using Persistance.Data.Context;
using Persistance.UnitOfWorks;
using Services;
using Servies.Abstractions;
using Shared.ErrorModels;
using Store.G04.Api.Extensions;
using Store.G04.Api.Middlewares;

namespace Store.G04.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            builder.Services.RegisterAllServieces(builder.Configuration);

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            await app.ConfigureMiddleWares();
        }
    }
}
