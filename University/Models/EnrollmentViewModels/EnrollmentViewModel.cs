using System.ComponentModel;
namespace University.Models.EnrollmentViewModels
{
    public class EnrollmentViewModel
    {
        public int Id { get; set; }

        [DisplayName("Student")]
        public int StudentId { get; set; }
        public string? Student { get; set; }
        
        [DisplayName("Course assignment")]
        public int CourseAssignmentId { get; set; }
        public string? CourseAssignment { get; set; }
    }
}
