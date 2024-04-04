using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWeb.Entities
{
    [Table(nameof(Student))]
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public Student()
        {
            Enrollments = new List<Enrollment>();
        }
    }
}
