using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DuplicatedEmailBadRequsetException(string email):BadRequsetException($"There Are Another User Use This Email : {email} ")
    {
    }
}
