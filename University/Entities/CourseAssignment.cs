using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWeb.Entities
{
    [Table(nameof(CourseAssignment))]
    [DisplayName("Course assignment")]
    public class CourseAssignment
    {
        public int Id { get; set; }
        public string Room { get; set; }
        public int CourseId { get; set; }

        public Course? Course { get; set; }

        public int InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public virtual ICollection<Enrollment> Enrollments{ get; set; }
        public CourseAssignment()
        {
            Enrollments = new List<Enrollment>();
        }
    }
}
