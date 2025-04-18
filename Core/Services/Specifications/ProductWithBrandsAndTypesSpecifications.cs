using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecificions<Product, int>
    {
        public ProductWithBrandsAndTypesSpecifications(int id) : base(P => P.Id == id)
        {
            ApplInclude();

        }
        public ProductWithBrandsAndTypesSpecifications(int? brandId, int? typeId, string? sort, int pageIndex , int pageSize )
           : base(
                 P =>
                 (!brandId.HasValue||P.BrandId == brandId )&&
                 (!typeId.HasValue||P.TypeId==typeId)
            
            )
        {
            ApplInclude();
          ApplySorting( sort );
            ApplyPagination(pageIndex, pageSize);
        }

        private void ApplInclude()
        {

            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }

        private void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "nameasc":
                        AddOrderBy(P => P.Name);
                        break;
                    case "namedesc":
                        AddOrderByDescending(P => P.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;

                }

            }
            else
            {
                AddOrderBy(P => P.Name);

            }
        }
        }

}
