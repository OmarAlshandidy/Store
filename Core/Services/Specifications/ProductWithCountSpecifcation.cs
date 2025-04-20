using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Specifications
{
    public class ProductWithCountSpecifcation :BaseSpecificions<Product,int>
    {
        public ProductWithCountSpecifcation(ProductSpecificationsParameters specParams) : base(
            P =>
                 (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search.ToLower())) &&
                 (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId) &&
                 (!specParams.TypeId.HasValue || P.TypeId == specParams.TypeId)
            )
        { 
            
        }
    }
}
