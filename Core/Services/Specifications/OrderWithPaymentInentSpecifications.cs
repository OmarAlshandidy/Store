using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModels;

namespace Services.Specifications
{
    public class OrderWithPaymentInentSpecifications:BaseSpecificions<Order,Guid>
    {
        public OrderWithPaymentInentSpecifications(string paymentIntentId):base(O=>O.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
