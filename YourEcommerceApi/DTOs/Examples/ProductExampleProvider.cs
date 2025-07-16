using Swashbuckle.AspNetCore.Filters;
using YourEcommerceApi.Models;

namespace YourEcommerceApi.DTOs.Product.Examples
{
    public class ProductExampleProvider : IExamplesProvider<ProductCreateDto>
    {
        public ProductCreateDto GetExamples()
        {
            return new ClothCreateDto
            {
                Name = "Remera Deportiva",
                Description = "Remera de algodón para deporte",
                Price = 1200,
                Stock = 20,
                BrandId = 1,
                SubcategoryId = 2,
                Gender = (Gender)1,
                SportId = 5
            };
        }
    }
}