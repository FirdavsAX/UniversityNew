using University.Models.EnrollmentViewModels;
using UniversityWeb.Entities;

namespace University.Mappings
{
    public static class EnrollmentMappings
    {
        public static EnrollmentViewModel ConvertToViewModel(this Enrollment enrollment)
        {
            var model = new EnrollmentViewModel()
            {
                Id= enrollment.Id,
                StudentId= enrollment.StudentId,
                Student = enrollment.Student.FirstName + " " + enrollment.Student?.LastName ?? "",
                CourseAssignmentId= enrollment.CourseAssignmentId,
                CourseAssignment = enrollment.CourseAssignment.ConvertToViewModel().Definition,
            };
            
            return model;
        }

        public static Enrollment ConvertToEnrollment(this EnrollmentViewModel enrollment)
        {
            return new Enrollment()
            {
                Id= enrollment.Id,
                StudentId= enrollment.StudentId,
                CourseAssignmentId = enrollment.CourseAssignmentId,
            };
        }
    }
}
