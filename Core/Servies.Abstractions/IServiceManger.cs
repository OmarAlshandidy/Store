﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servies.Abstractions
{
    public interface IServiceManger
    {
      IProductService productService { get; }
    }
}
