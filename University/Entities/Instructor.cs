using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWeb.Entities
{
    [Table(nameof(Instructor))]
    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public virtual ICollection<CourseAssignment> CourseAssignments { get; set; }
        public Instructor()
        {
            CourseAssignments = new List<CourseAssignment>();
        }
    }
}
