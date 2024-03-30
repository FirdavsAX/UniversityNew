using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWeb.Entities
{
    public class Enrollment
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public Student? Student { get; set; }


        public int CourseAssignmentId { get; set; }
        [ForeignKey(nameof(CourseAssignmentId))]

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public CourseAssignment? CourseAssignment { get; set; }
    }
}
