using Microsoft.Extensions.Logging;
using Store.Core.Entities;
using Store.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Repository.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (context.ProductBrands != null && !context.ProductBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Store.Repository/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    if (brands != null)
                        await context.ProductBrands.AddRangeAsync(brands);

                }

                if (context.ProductCategories != null && !context.ProductCategories.Any())
                {
                    var typesData = File.ReadAllText("../Store.Repository/Data/SeedData/categories.json");
                    var types = JsonSerializer.Deserialize<List<ProductCategory>>(typesData);
                    if (types != null)
                        await context.ProductCategories.AddRangeAsync(types);
                }

                if (context.Products != null && !context.Products.Any())
                {
                    var productData = File.ReadAllText("../Store.Repository/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productData);
                    if (products != null)
                        await context.Products.AddRangeAsync(products);
                }
                await context.SaveChangesAsync();




            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
