using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Servies.Abstractions
{
    public interface IProductService
    {
        // Get All Product 
        Task<IEnumerable<ProductResultDto>> GetAllProductAsync();

        // Get All Product By Id 
        Task<ProductResultDto?> GetProductAsync(int id);

        // Get All Brands 
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        // Get All Types 

        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();

    }
}
