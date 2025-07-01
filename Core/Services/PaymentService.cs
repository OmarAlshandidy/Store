using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.OrderModels;
using Microsoft.Extensions.Configuration;
using Servies.Abstractions;
using Shared;
using Stripe;
using OrderProduct = Domain.Models.Product;

namespace Services
{
    public class PaymentService(
        IMapper mapper,
        IBasketRepository basketRepository
        ,IUnitOfWork unitOfWork,
        IConfiguration configuration) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            var basket = await basketRepository.GetBasketAsync(basketId);
            if (basket is null) throw new BasketNotFoundException(basketId);
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<OrderProduct, int>().GetAsync(item.Id);
                if(product is null) throw new ProductNotFoundException(item.Id);

                item.Price = product.Price;

            }
            if (!basket.DeliveryMethodId.HasValue) throw new Exception("Invalid Dlivery Method Id !!");
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod,int>().GetAsync(basket.DeliveryMethodId.Value);
            if (deliveryMethod is null) throw new DelivaryMethodNotFoundException(basket.DeliveryMethodId.Value);
            basket.ShippingPrice = deliveryMethod.Cost;

            //Amount 
            var amount = (long)(basket.Items.Sum(I => I.Price * I.Quantity) + basket.ShippingPrice)*100;
            StripeConfiguration.ApiKey = configuration["StripeSettings:SecertKey"];
            var service = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                //Create
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string>() { "card" }

                };
               var paymentIntent =  await service.CreateAsync(createOptions);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                //Update 
                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                   

                };
                 await service.UpdateAsync(basket.PaymentIntentId,updateOptions);
               
            }
            
            await basketRepository.UpdateBasketAsync(basket);
            var result = mapper.Map<BasketDto>(basket);
            return result;
            

        }
    }
}
