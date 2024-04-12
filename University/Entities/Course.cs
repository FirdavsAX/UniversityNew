using System.ComponentModel.DataAnnotations.Schema;
using University.Entities;
namespace UniversityWeb.Entities
{
    [Table(nameof(Course))]
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Hours { get; set; }
        public int CategoryId { get; set; }
        public Category Category{ get; set; }
        public virtual ICollection<CourseAssignment> CourseAssignments { get; set; }
        public Course()
        {
            CourseAssignments = new List<CourseAssignment>();
        }
    }
}
