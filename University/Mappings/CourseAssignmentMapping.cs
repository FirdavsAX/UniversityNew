using University.Models.CourseAssignmentViewModels;
using UniversityWeb.Entities;

namespace University.Mappings
{
    public static class CourseAssignmentMapping
    {
        public static CourseAssignment ConvertToCourseAssignment(this CourseAssignmentDisplayViewModel viewModel)
        {
            return new CourseAssignment()
            {
                Id = viewModel.Id,
                Room = viewModel.Room,
                InstructorId = viewModel.InstructorId,
                CourseId = viewModel.CourseId,
            };
        }
        public static CourseAssignmentDisplayViewModel ConvertToViewModel(this CourseAssignment assignment)
        {
            return new CourseAssignmentDisplayViewModel()
            {
                Id = assignment.Id,
                Room = assignment.Room,
                InstructorId = assignment.InstructorId,
                CourseId = assignment.CourseId,
                Course = assignment.Course?.Name ?? "none",
                Instructor = (assignment.Instructor?.FirstName ?? "none") + " " + (assignment.Instructor?.LastName ?? "none")   ,
            };
        }
    }
}
