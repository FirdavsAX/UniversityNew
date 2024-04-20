using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Exceptions;
using University.Mappings;
using University.Models.CourseViewModel;
using University.Services.CourseSerives;
using UniversityWeb.Entities;

namespace University.Services.CategoryServices
{
    public class CourseService : ICourseService
    {
        private readonly UniversityDbContext _context;
        public CourseService(UniversityDbContext _context)
        {
            this._context = _context;
        }

        public CourseDisplayViewModel Create(CourseDisplayViewModel course)
        {
            if(course == null)
            {   
                throw new ArgumentNullException(nameof(course));
            }
            var createdEntity = _context.Courses.Add(course.ConvertToCourse());
            _context.SaveChanges();

            return createdEntity.Entity.ConvertToViewModel();
        }

        public async Task<CourseDisplayViewModel> CreateAsync(CourseDisplayViewModel course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            var createdEntity = await _context.Courses.AddAsync(course.ConvertToCourse());
            await _context.SaveChangesAsync();

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == course.CategoryId);
            createdEntity.Entity.Category = category;
            
            return createdEntity.Entity.ConvertToViewModel();
        }

        public void Delete(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                throw new CourseNotFoundException($"Course {id} is not found");
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                throw new CourseNotFoundException($"Course {id} is not found");
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public CourseDisplayViewModel GetById(int id)
        {
            var course = _context.Courses.FirstOrDefault(c => c.Id == id)
                ?? throw new CourseNotFoundException($"Course {id} is not found");

            return course.ConvertToViewModel();
        }

        public async Task<CourseDisplayViewModel> GetByIdAsync(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id)
                ?? throw new CourseNotFoundException($"Course {id} is not found");

            return course.ConvertToViewModel();
        }

        public ICollection<CourseDisplayViewModel> GetCourses(string? searchString = "", int? categoryId = 0, string? sortOrder = "name_asc")
        {
            var query = _context.Courses.Include(c => c.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(i => i.Name.Contains(searchString) || i.Category.Name.Contains(searchString));
            }
            if (categoryId.HasValue && categoryId != 0)
            {
                query = query.Where(i => i.Category.Id == categoryId);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(i => i.Name); break;

                case "price_asc":
                    query = query.OrderBy(i => i.Price); break;

                case "price_desc":
                    query = query.OrderByDescending(i => i.Hours); break;

                case "hours_asc":
                    query = query.OrderBy(i => i.Hours); break;
                case "hours_desc":
                    query = query.OrderByDescending(i => i.Hours); break;
                case "category_asc":
                    query = query.OrderBy(i => i.Category.Name); break;
                case "category_desc":
                    query = query.OrderByDescending(i => i.Category.Name); break;
                default:
                    query = query.OrderBy(i => i.Name); break;
            }

            return query.Select(c => c.ConvertToViewModel()).ToList(); 
        }

        public async Task<ICollection<CourseDisplayViewModel>> GetCoursesAsync(string? searchString = "", int? categoryId = 0, string? sortOrder = "name_asc")
        {
            var query = _context.Courses.Include(c => c.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(i => i.Name.Contains(searchString) || i.Category.Name.Contains(searchString));
            }
            if (categoryId.HasValue && categoryId != 0)
            {
                query = query.Where(i => i.Category.Id == categoryId);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(i => i.Name); break;

                case "price_asc":
                    query = query.OrderBy(i => i.Price); break;

                case "price_desc":
                    query = query.OrderByDescending(i => i.Hours); break;

                case "hours_asc":
                    query = query.OrderBy(i => i.Hours); break;
                case "hours_desc":
                    query = query.OrderByDescending(i => i.Hours); break;
                case "category_asc":
                    query = query.OrderBy(i => i.Category.Name); break;
                case "category_desc":
                    query = query.OrderByDescending(i => i.Category.Name); break;
                default:
                    query = query.OrderBy(i => i.Name); break;
            }

            return await query.Select(c => c.ConvertToViewModel()).ToListAsync();
        }

        public CourseDisplayViewModel Update(CourseDisplayViewModel course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            var entity = _context.Courses.FirstOrDefault(c => c.Id == course.Id) 
                ?? throw new CourseNotFoundException();
            
            var updatedEntity = _context.Courses.Attach(entity);
            _context.SaveChanges();

            return updatedEntity.Entity.ConvertToViewModel();
        }

        public async Task<CourseDisplayViewModel> UpdateAsync(CourseDisplayViewModel course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            var entity = await _context.Courses.FirstOrDefaultAsync(c => c.Id == course.Id)
                ?? throw new CourseNotFoundException();

            var updatedEntity = _context.Courses.Attach(entity);
            await _context.SaveChangesAsync();

            return updatedEntity.Entity.ConvertToViewModel();
        }
    }
}
