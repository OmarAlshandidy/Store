﻿using System;
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
        //Task<IEnumerable<ProductResultDto>> GetAllProductAsync(int? brandId, int? typeId, string? sort, int pageIndex = 1, int pageSize = 5);
        Task<PaginationResponse<ProductResultDto>> GetAllProductAsync(ProductSpecificationsParameters specParams);

        // Get All Product By Id 
        Task<ProductResultDto?> GetProductAsync(int id);

        // Get All Brands 
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        // Get All Types 

        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();

    }
}
