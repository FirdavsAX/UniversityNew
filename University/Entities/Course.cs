using System.ComponentModel.DataAnnotations.Schema;
namespace UniversityWeb.Entities
{
    [Table(nameof(Course))]
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Hours { get; set; }
        public virtual ICollection<CourseAssignment> CourseAssignments { get; set; }
        public Course()
        {
            CourseAssignments = new List<CourseAssignment>();
        }
    }
}
