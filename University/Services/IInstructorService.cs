using University.Models.InstructorViewModels;

namespace University.Services
{
    public interface IInstructorService
    {
        Task<InstructorDisplayViewModel> Create(InstructorActionViewModel instructor);
        Task<InstructorActionViewModel> Update(InstructorActionViewModel instructor);
        Task<InstructorDisplayViewModel> GetById(int id);
        Task<IEnumerable<InstructorDisplayViewModel>> GetInstructors(
            string searchString = "",
            string sortOrder = "name_asc",
            int? departmentId = 0
            );
        Task Delete(int id);
    }
}
