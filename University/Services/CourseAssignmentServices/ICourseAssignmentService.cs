using University.Models.CourseAssignmentViewModels;
using UniversityWeb.Entities;

namespace University.Services.CourseAssignmentServices
{
    public interface ICourseAssignmentService
    {
        Task<ICollection<CourseAssignmentDisplayViewModel>> GetCourseAssignmentsAsync(
            int? instructorId = 0, int? courseId = 0 ,
            string? searchString="",
            string? sortOrder="room_asc");
        Task<CourseAssignmentDisplayViewModel> CreateAsync(CourseAssignmentDisplayViewModel viewModel);
        Task<CourseAssignmentDisplayViewModel> UpdateAsync(CourseAssignmentDisplayViewModel viewModel);
        Task DeleteAsync(int id);
        Task<CourseAssignmentDisplayViewModel> GetByIdAsync(int id); 
    }
}
