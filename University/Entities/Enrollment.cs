using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWeb.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseAssignmentId { get; set; }
        public CourseAssignment? CourseAssignment { get; set; }
    }
}
