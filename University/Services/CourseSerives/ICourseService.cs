using University.Models.CourseViewModel;

namespace University.Services.CourseSerives
{
    public interface ICourseService
    {
        Task<ICollection<CourseDisplayViewModel>> GetCoursesAsync(string? searchString = "", int? categoryId = 0, string? sortOrder = "name_asc");
        Task<CourseDisplayViewModel> CreateAsync(CourseDisplayViewModel course);
        Task<CourseDisplayViewModel> UpdateAsync(CourseDisplayViewModel course);
        Task<CourseDisplayViewModel> GetByIdAsync(int id);
        Task DeleteAsync(int id);

        ICollection<CourseDisplayViewModel> GetCourses(string? searchString = "", int? categorytId = 0, string? sortOrder = "name_asc");
        CourseDisplayViewModel Create(CourseDisplayViewModel course);
        CourseDisplayViewModel Update(CourseDisplayViewModel course);
        CourseDisplayViewModel GetById(int id);
        void Delete(int id);
    }
}
