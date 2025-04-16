using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistance.Data.Context;

namespace Persistance
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;

        public DbInitializer(StoreDbContext context)
        {
                    _context = context;
        }
        public async Task InitializeAsync()
        {
            try
            {

           
            // Create DataBase If It Dosen`t Exists && Apply To Any Pending Migrations
            if (_context.Database.GetPendingMigrations().Any())
            {
               await _context.Database.MigrateAsync();
            }
            // Data Seeding 
            #region Seeding Product Types
            // Seeding ProductTypes  From Json Files
            if (!_context.productTypes.Any())
            {
                // 1.Read All Data From Types Json Files As String
           var typesData  = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Seeding\types.json");
                // 2. Transform string To C# Object [list<ProductTypes>]
            var types  = JsonSerializer.Deserialize<List<ProductType>>(typesData);


                // 3. Add   List<ProductTypes> TO DataBase  
                if(types is not null && types.Any())
                {
                    await _context.productTypes.AddRangeAsync(types);
                    await _context.SaveChangesAsync();
                }

            }



            #endregion

            #region seeding Prouduct Brands
            // Seeding ProductBrands  From Json Files
            if (!(await _context.productBrands.AnyAsync()))
            {
                // 1.Read All Data From Types Json Files As String
                var brandsDate = await File.ReadAllTextAsync(@"../Infrastructure\Persistance\Seeding\brands.json");
                // 2. Transform string To C# Object [list<ProductTypes>]
                var brands =  JsonSerializer.Deserialize<List<ProductBrand>>(brandsDate);
                // 3. Add   List<ProductTypes> TO DataBase  
                if (brands is not null && brands.Any())
                {
                    await _context.productBrands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }

            }

            #endregion

            #region Seeding Product 
            // Seeding Products From Json Files
            if (!_context.Products.Any())
            {
                // 1.Read All Data From  Products.Json  Files As String 
               var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Seeding\products.json");
                // 2. Transform string To  C# Object [list<Products>]
                var product = JsonSerializer.Deserialize<List<Product>>(productsData);
                //3. Add List<product> To DataBase  
                if(product is not null && product.Any())
                {
                    await _context.Products.AddRangeAsync(product);
                    await _context.SaveChangesAsync();
                }
            }
                #endregion


            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
