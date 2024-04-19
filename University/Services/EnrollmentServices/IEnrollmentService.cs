using University.Models.EnrollmentViewModels;

namespace University.Services.EnrollmentServices
{
    public interface IEnrollmentService
    {
        Task<IEnumerable<EnrollmentViewModel>> GetEnrollments();
        Task<EnrollmentViewModel> CreateAsync(EnrollmentViewModel enrollment);
        Task<EnrollmentViewModel> UpdateAsync(EnrollmentViewModel enrollment);
        Task DeleteAsync(int id);
        Task<EnrollmentViewModel> GetById(int id);
    }
}
