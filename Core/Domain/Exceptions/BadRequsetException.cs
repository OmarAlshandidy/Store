﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
   public abstract  class BadRequsetException(string message) : Exception(message)
    {
    }
}
