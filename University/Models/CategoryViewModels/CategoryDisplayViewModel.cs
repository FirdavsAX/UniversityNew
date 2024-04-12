using University.Entities;
using UniversityWeb.Entities;

namespace University.Models.CategoryViewModels
{
    public class CategoryDisplayViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public string? Parent { get; set; }
        public ICollection<Category>? ChildCategories { get; set; }
        public ICollection<Course>? Courses { get; set; }
    }
}
