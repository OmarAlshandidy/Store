using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Services.Services;
using Servies.Abstractions;

namespace Services
{
    public class ServiceManeger(IUnitOfWork unitOfWork , IMapper mapper
        ,IBasketRepository basketRepository) : IServiceManger
    {
        public IProductService productService { get; }  = new ProductService(unitOfWork,mapper);

        public IBasketService basketService { get; } = new BasketService(basketRepository, mapper);
    }
}
