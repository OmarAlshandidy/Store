using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.OrderModels;

namespace Servies.Abstractions
{
    public interface IOrderService
    {
        Task<OrderResualtDto> GetOrderByIdAsync(Guid id);
       
        Task<IEnumerable<OrderResualtDto>> GetOrderByUserEmailAsync(string userEmail);
        //Create Order 

       Task<OrderResualtDto> CreateOrderAsync(OrderRequestDto orderRequest,string usrrEmail);

        //Get All Delivary Methods 

        Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods();

    }
}
