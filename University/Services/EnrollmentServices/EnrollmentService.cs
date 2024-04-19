using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Mappings;
using University.Models.EnrollmentViewModels;

namespace University.Services.EnrollmentServices
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly UniversityDbContext _context;
        public EnrollmentService(UniversityDbContext _context)
        {
            this._context = _context;
        }
        public async Task<EnrollmentViewModel> CreateAsync(EnrollmentViewModel enrollment)
        {
            ArgumentNullException.ThrowIfNull(enrollment);

            var entity = _context.Enrollments.Add(enrollment.ConvertToEnrollment());
            await _context.SaveChangesAsync();

            return await GetById(entity.Entity.Id);
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Enrollments.
               FirstOrDefaultAsync(i => i.Id == id);
            
            ArgumentNullException.ThrowIfNull(entity);

            _context.Enrollments.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<EnrollmentViewModel> GetById(int id)
        {
            var entity = await _context.Enrollments
                .Include(i => i.CourseAssignment.Course)
                .Include(i => i.CourseAssignment.Instructor)
                .Include(i => i.Student)
                .FirstOrDefaultAsync(i => i.Id == id);

            ArgumentNullException.ThrowIfNull(entity);

            return entity.ConvertToViewModel();
        }

        public async Task<IEnumerable<EnrollmentViewModel>> GetEnrollments()
        {
            var query = _context.Enrollments
                .Include(i => i.CourseAssignment.Instructor)
                .Include(i => i.CourseAssignment.Course)
                .Include(i => i.Student)
                .AsQueryable();

            return await query.Select(i => i.ConvertToViewModel()).ToListAsync();
        }

        public async Task<EnrollmentViewModel> UpdateAsync(EnrollmentViewModel enrollment)
        {
            ArgumentNullException.ThrowIfNull(enrollment);

            var entity = await _context.Enrollments
                .Include(i => i.CourseAssignment.Instructor)
                .Include(i => i.CourseAssignment.Course)
                .Include(i => i.Student)
                .FirstOrDefaultAsync(i => i.Id ==  enrollment.Id);
            
            ArgumentNullException.ThrowIfNull(entity);

            _context.Enrollments.Attach(entity);
            await _context.SaveChangesAsync();

            return entity.ConvertToViewModel();
        }
    }
}
