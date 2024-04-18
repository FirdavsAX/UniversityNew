using System.ComponentModel;
using UniversityWeb.Entities;
namespace University.Models.CourseAssignmentViewModels
{
    public class CourseAssignmentDisplayViewModel
    {
        public int Id { get; set; }
        public string Room { get; set; }
        public int Hours { get; set; }
        [DisplayName("Instructor")]
        public int InstructorId { get; set; }
        public string? Instructor { get; set; }
        [DisplayName("Course")]
        public int CourseId { get; set; }
        public string? Course { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public CourseAssignmentDisplayViewModel()
        {
            Enrollments = new List<Enrollment>();
        }
    }
}
