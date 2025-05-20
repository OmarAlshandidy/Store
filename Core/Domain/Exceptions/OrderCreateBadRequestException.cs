using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class OrderCreateBadRequestException() : BadRequsetException("Invalid Operation When Create Or Order!!")
    {
    }
}
