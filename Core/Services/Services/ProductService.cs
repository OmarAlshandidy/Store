using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Specifications;
using Servies.Abstractions;
using Shared;

namespace Services.Services
{
    public class ProductService(IUnitOfWork unitOfWork,IMapper mapper) : IProductService
    {

        // Get All Products
        public async Task<IEnumerable<ProductResultDto>> GetAllProductAsync(int? brandId, int? typeId, string? sort, int pageIndex = 1, int pageSize = 5)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(brandId,typeId,sort,pageIndex , pageSize);
            // Get All Products Throught ProductRepository
           var products =  await unitOfWork.GetRepository<Product,int>().GetAllAsync(spec);
            // Mapping IEnumerable<Product> To Task<IEnumerable<ProductResultDto>> : Auto Mapper 
             var result = mapper.Map<IEnumerable<ProductResultDto>>(products);

            return result;
        }
        // Get Product By Id 
        public async Task<ProductResultDto?> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(id);
               var product = await unitOfWork.GetRepository<Product,int>().GetAsync(spec);
            if (product is null) return null;
            var result = mapper.Map<ProductResultDto>(product);
            return result;
        }
        // Get All Brands 
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
           var brands = await unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();

            return mapper.Map<IEnumerable<BrandResultDto>>(brands);
        }

        // Get All Types
        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
           var types = await unitOfWork.GetRepository<ProductType,int>().GetAllAsync();
            return mapper.Map<IEnumerable<TypeResultDto>>(types);
        }
    }
}
