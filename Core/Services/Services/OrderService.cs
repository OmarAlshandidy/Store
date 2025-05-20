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
using Services.Specifications;
using Servies.Abstractions;
using Shared.OrderModels;

namespace Services.Services
{
    public class OrderService(
        IMapper mapper
        ,IBasketRepository basketRepository
        ,IUnitOfWork unitOfWork
        
        ) : IOrderService
    {
        public async Task<OrderResualtDto> CreateOrderAsync(OrderRequestDto orderRequest, string userEmail)
        {
            ///1.Addresss 
            var address = mapper.Map<Address>(orderRequest.ShipToAddress);
            // 2.Order Item => Basket 
            var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId);
            if(basket is null) throw new BasketNotFoundException(orderRequest.BasketId);    

            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await unitOfWork.GetRepository<Product, int>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);
                var orderItem = new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl),item.Quantity,product.Price);
                orderItems.Add(orderItem);
            }
            // 3. Get Delivery Method 
            var delivaryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(orderRequest.DeliveryMethodId);
            if (delivaryMethod is null) throw new DelivaryMethodNotFoundException(orderRequest.DeliveryMethodId);
            // 4. Sub Total 
            var subTotal = orderItems.Sum(i=>i.Price * i.Quantity);
            // 5. Todo : Create Payment Intent Id ---- 

            // Create Order 
            var order = new Order(userEmail, address,orderItems, delivaryMethod, subTotal, "");
              await unitOfWork.GetRepository<Order,Guid>().AddAsync(order);
            var count =   await unitOfWork.SaveChangesAsync();
            if (count == 0) throw new OrderCreateBadRequestException();
            var result = mapper.Map<OrderResualtDto>(order);
            return result;
        } 

        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods()
        {
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            var reuslt = mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethod);
            return reuslt;
        }

        public async Task<OrderResualtDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecifications(id);
            var order = await unitOfWork.GetRepository<Order,Guid>().GetAsync(spec);
            if (order is null) throw new OrderNotFoundException(id);
            var result = mapper.Map<OrderResualtDto>(order);
            return result;
        }

        public async Task<IEnumerable<OrderResualtDto>> GetOrderByUserEmailAsync(string userEmail)
        {
            var spec = new OrderSpecifications(userEmail);
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            var result = mapper.Map< IEnumerable<OrderResualtDto>> (order);
            return result;
        }
    }
}
