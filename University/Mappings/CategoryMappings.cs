using University.Entities;
using University.Models.CategoryViewModels;

namespace University.Mappings
{
    public static class CategoryMappings
    {
        public static Category ConvertToCategory(this CategoryDisplayViewModel viewModel)
        {
            Category category;
            try
            {
                category = new Category()
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    ParentId = viewModel.ParentId 
                };

            }
            catch(Exception ex)
            {
                throw ex;
            }
            return category;
        }
        public static CategoryDisplayViewModel ConvertToViewModel(this Category category)
        {
            CategoryDisplayViewModel viewModel;
            try
            {
                viewModel = new CategoryDisplayViewModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    ParentId = category.ParentId ?? 0,
                };
                if(category.Parent != null)
                {
                    viewModel.Parent = category.Parent.Name;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return viewModel;
        }
    }
}
