using UniversityWeb.Entities;

namespace University.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public ICollection<Category>? ChildCategories { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public Category()
        {
            ChildCategories = new List<Category>();
            Courses = new List<Course>();
        }

    }
}
