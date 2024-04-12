using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Entities;
using University.Exceptions;
using University.Mappings;
using University.Models.CategoryViewModels;

namespace University.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        public UniversityDbContext _context { get; set; }
        public CategoryService(UniversityDbContext _context)
        {
            this._context = _context;
        }
        public CategoryDisplayViewModel CreateCategory(CategoryDisplayViewModel category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            if (category.ParentId == 0)
            {
                category.ParentId = null;
            } 
            var createdCategory = _context.Categories.Add(category.ConvertToCategory());

            _context.SaveChanges();

            return createdCategory.Entity.ConvertToViewModel();
        }

        public async Task<CategoryDisplayViewModel> CreateCategoryAsync(CategoryDisplayViewModel category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            if (category.ParentId == 0)
            {
                category.ParentId = null;
            }
            var createdCategory = _context.Categories.Add(category.ConvertToCategory());
            await _context.SaveChangesAsync();

            return createdCategory.Entity.ConvertToViewModel();
        }

        public void Delete(CategoryDisplayViewModel category)
        {
            if (category is null)
            {
                throw new CategoryNotFoundException($"Category {category.Id} is not found!");
            }

            var entity = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (entity is null)
            {
                throw new CategoryNotFoundException($"Category {category.Id} is not found!");
            }

            _context.Categories.Remove(entity);
            _context.SaveChanges();

        }

        public async Task DeleteAsync(CategoryDisplayViewModel category)
        {
            if (category is null)
            {
                throw new CategoryNotFoundException($"Category {category.Id} is not found!");
            }

            var entity = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (entity is null)
            {
                throw new CategoryNotFoundException($"Category {category.Id} is not found!");
            }

            _context.Categories.Remove(entity);
            await _context.SaveChangesAsync();

        }

        public CategoryDisplayViewModel GetById(int id)
        {
            var entity = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (entity is null)
            {
                throw new CategoryNotFoundException($"Category {id} is not found!");
            }

            return entity.ConvertToViewModel();
        }

        public async Task<CategoryDisplayViewModel> GetByIdAsync(int id)
        {
            var entity = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (entity is null)
            {
                throw new CategoryNotFoundException($"Category {id} is not found!");
            }

            return entity.ConvertToViewModel();
        }

        public ICollection<CategoryDisplayViewModel> GetCategories(string? searchString = "", int? parentId = null, string? sortOrder = "name_asc")
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(c => c.Name.Contains(searchString) || c.Description.Contains(searchString));//ParentId contains Having ???/*das*/
            }
            if (parentId.HasValue || parentId != 0)
            {
                query = query.Where(c => c.ParentId == parentId);
            }
            _ = sortOrder switch
            {
                "name_desc" => query = query.OrderByDescending(c => c.Name),
                "description_asc" => query = query.OrderBy(c => c.Description),
                "description_desc" => query = query.OrderByDescending(c => c.Description),
                "parent_asc" => query = query.OrderBy(c => c.Parent.Name),
                "parent_desc" => query = query.OrderByDescending(c => c.Parent.Name),
                _ => query = query.OrderBy(c => c.Name),
            };
            return query.Select(c => c.ConvertToViewModel()).ToList();
        }

        public async Task<ICollection<CategoryDisplayViewModel>> GetCategoriesAsync(string? searchString = "", int? parentId = null, string? sortOrder = "name_asc")
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(c => c.Name.Contains(searchString) || c.Description.Contains(searchString));//ParentId contains Having ???/*das*/
            }
            if (parentId.HasValue && parentId != 0)
            {
                query = query.Where(c => c.ParentId == parentId);
            }
            _ = sortOrder switch
            {
                "name_desc" => query = query.OrderByDescending(c => c.Name),
                "description_asc" => query = query.OrderBy(c => c.Description),
                "description_desc" => query = query.OrderByDescending(c => c.Description),
                "parent_asc" => query = query.OrderBy(c => c.Parent.Name),
                "parent_desc" => query = query.OrderByDescending(c => c.Parent.Name),
                _ => query = query.OrderBy(c => c.Name),
            };
            return await query.Select(c => c.ConvertToViewModel()).AsNoTracking().ToListAsync();
        }

        public CategoryDisplayViewModel UpdateCategory(CategoryDisplayViewModel category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            if (CategoryExist(category.Id))
            {
                throw new CategoryNotFoundException($"Category {category.Id} not found!");
            }

            var updatedCategory = _context.Categories.Update(category.ConvertToCategory());
            _context.SaveChanges();

            return updatedCategory.Entity.ConvertToViewModel();

        }

        public async Task<CategoryDisplayViewModel> UpdateCategoryAsync(CategoryDisplayViewModel category)
        {
            if (category is null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            if (CategoryExist(category.Id))
            {
                throw new CategoryNotFoundException($"Category {category.Id} not found!");
            }

            var updatedCategory = _context.Categories.Attach(category.ConvertToCategory());
            await _context.SaveChangesAsync();

            return updatedCategory.Entity.ConvertToViewModel();
        }
        private bool CategoryExist(int id) => _context.Categories.Any(i => i.Id == id);
    }
}
