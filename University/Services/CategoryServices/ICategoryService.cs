using University.Entities;
using University.Models.CategoryViewModels;

namespace University.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryDisplayViewModel>> GetCategoriesAsync(string? searchString = "", int? parentId = 0,string? sortOrder="name_asc");
        Task<CategoryDisplayViewModel> CreateCategoryAsync(CategoryDisplayViewModel category);
        Task<CategoryDisplayViewModel> UpdateCategoryAsync(CategoryDisplayViewModel category);
        Task<CategoryDisplayViewModel> GetByIdAsync(int id);
        Task DeleteAsync(CategoryDisplayViewModel category);

        ICollection<CategoryDisplayViewModel> GetCategories(string? searchString = "", int? parentId = 0 , string? sortOrder = "name_asc");
        CategoryDisplayViewModel CreateCategory(CategoryDisplayViewModel category);
        CategoryDisplayViewModel UpdateCategory(CategoryDisplayViewModel category);
        CategoryDisplayViewModel GetById(int id);
        void Delete(CategoryDisplayViewModel category);

    }
}
