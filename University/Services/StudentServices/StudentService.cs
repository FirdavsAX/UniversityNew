using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Exceptions;
using University.Mappings;
using University.Models.StudentViewModels;

namespace University.Services.StudentServices
{
    public class StudentService:IStudentService
    {
        private readonly UniversityDbContext _context;
        public StudentService(UniversityDbContext _context)
        {
            this._context = _context;
        }

        public async Task<StudentDisplay> CreateAsync(StudentInAction student)
        {
            var entity = student.ConvertToStudent();
            if (entity is null)
            {
                throw new EntityNotFoundException();
            }

            var createdInstructor = _context.Students.Add(entity);
            await _context.SaveChangesAsync();

            return createdInstructor.Entity.ConvertToViewModel();
        }

        public async Task Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(i => i.Id == id);

            if (student is null)
            {
                throw new EntityNotFoundException("Entity not found!");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<StudentDisplay> GetById(int id)
        {
            var entity = _context.Students
                .FirstOrDefault(i => i.Id == id);

            if (entity is null)
            {
                throw new EntityNotFoundException();
            }

            return entity.ConvertToViewModel();
        }
        public async Task<StudentInAction> GetByIdToAction(int id)
        {
            var entity = _context.Students
                .FirstOrDefault(i => i.Id == id);

            if (entity is null)
            {
                throw new EntityNotFoundException();
            }

            return entity.ConvertToStudentAction();
        }
        public async Task<IEnumerable<StudentDisplay>> GetInstructors(string? searchString, string? sortOrder)
        {
            var query = _context.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(i => i.FirstName.Contains(searchString) || i.LastName.Contains(searchString)
                    || i.Email.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(i => i.FirstName); break;
                case "email_asc":
                    query = query.OrderBy(i => i.Email); break;

                case "email_desc":
                    query = query.OrderByDescending(i => i.Email).ThenByDescending(i => i.LastName); break;
                default:
                    query = query.OrderBy(i => i.FirstName).ThenBy(i => i.LastName); break;
            }
            var students = await query.Select(i => i.ConvertToViewModel()).ToListAsync();

            return students;
        }

        public async Task<StudentInAction> Update(StudentInAction student)
        {
            if (!EntityExist(student.Id))
            {
                throw new EntityNotFoundException();
            }

            var entity = _context.Students.Update(student.ConvertToStudent());
            await _context.SaveChangesAsync();

            return entity.Entity.ConvertToStudentAction();
        }
        private bool EntityExist(int id)
        {
            return _context.Students.Any(i => i.Id == id);
        }
    }
}
