using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DelivaryMethodNotFoundException(int id) : NotFoundException($"Delivary Method With Id {id} Not Found")
    {
    }
}
