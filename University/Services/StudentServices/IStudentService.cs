using University.Models.InstructorViewModels;
using University.Models.StudentViewModels;

namespace University.Services.StudentServices
{
    public interface IStudentService
    {
        Task<StudentDisplay> CreateAsync(StudentInAction instructor);
        Task<StudentInAction> Update(StudentInAction instructor);
        Task<StudentDisplay> GetById(int id);
        Task<StudentInAction> GetByIdToAction(int id);
        Task<IEnumerable<StudentDisplay>> GetInstructors(
            string searchString = "",
            string sortOrder = "name_asc"
            );
        Task Delete(int id);
    }
}
