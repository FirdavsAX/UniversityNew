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
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required,MaxLength(10),MinLength(3)]
        public string Room { get; set; }

        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public Course? Course { get; set; }

        public int InstructorId { get; set; }

        [ForeignKey(nameof(InstructorId))]
        [DeleteBehavior(DeleteBehavior.NoAction)]

        public Instructor? Instructor { get; set; }

        public virtual ICollection<Enrollment> Enrollments{ get; set; }
        public CourseAssignment()
        {
            Enrollments = new List<Enrollment>();
        }
    }
}
