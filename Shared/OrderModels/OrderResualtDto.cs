using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public class OrderResualtDto
    {


        //Id 
        public Guid Id { get; set; }
        //User Email
        public string UserEmail { get; set; }

        // Shipping Address
        public AddressDto ShippingAddress { get; set; }

        public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>(); // Navigational Property 
        //Delivary Method 
        public string DeliveryMethod { get; set; } 
        public int? DelivaryMethodId { get; set; } 

        //PaymentStatus
        public string PaymentStatus { get; set; } 

        // SubTotal
        public decimal SubTotal { get; set; }

        //Order Date
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        //Payment 
        public string PaymentIntentId { get; set; }  = string.Empty;

        public decimal Total { get; set; }




    }
}

