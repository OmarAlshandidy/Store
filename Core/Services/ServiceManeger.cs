using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Services;
using Servies.Abstractions;

namespace Services
{
    public class ServiceManeger(IUnitOfWork unitOfWork , IMapper mapper
        ,IBasketRepository basketRepository
        ,ICashRepository cashRepository,
        UserManager<AppUser> userManager
        ) : IServiceManger
    {
        public IProductService productService { get; }  = new ProductService(unitOfWork,mapper);

        public IBasketService basketService { get; } = new BasketService(basketRepository, mapper);

        public ICashService cashService { get; } = new CashService(cashRepository);

        public IAuthService authService { get; } = new AuthService(userManager);
    } 
}
