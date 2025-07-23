using YourEcommerce.ViewModels;

namespace YourEcommerce.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryViewModel>> GetCategoriesAsync();
}