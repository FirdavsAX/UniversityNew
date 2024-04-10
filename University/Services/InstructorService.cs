using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using University.Data;
using University.Exceptions;
using University.Mappings;
using University.Models.InstructorViewModels;

namespace University.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly UniversityDbContext _context;
        public InstructorService(UniversityDbContext _context)
        {
            this._context = _context;
        }

        public async Task<InstructorDisplayViewModel> Create(InstructorActionViewModel instructor)
        {
            var entity = instructor.ConvertToInstructor();
            if (entity is null)
            {
                throw new EntityNotFoundException();
            }
            try
            {
                _context.Instructors.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception();
            }

            return null;
        }

        public async Task Delete(int id)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == id);

            if(instructor is null)
            {
                throw new EntityNotFoundException("Entity not found!");
            }

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();

        }

        public async Task<InstructorDisplayViewModel> GetById(int id)
        {
            var entity = _context.Instructors.FirstOrDefault(i => i.Id == id);

            if (entity is null)
            {
                throw new EntityNotFoundException();
            }

            return entity.ConvertToViewModel();
        }

        public async Task<IEnumerable<InstructorDisplayViewModel>> GetInstructors(int? departmentId, string? searchString,string? sortOrder)
        {
            var query = _context.Instructors.Include(i => i.Department).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(i => i.FirstName.Contains(searchString) || i.LastName.Contains(searchString)
                    || i.Email.Contains(searchString) || i.Department.Name.Contains(searchString));
            }
            if (departmentId.HasValue && departmentId != 0)
            {
                query = query.Where(i => i.DepartmentId == departmentId);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(i => i.FirstName); break;

                case "department_asc":
                    query = query.OrderBy(i => i.Department.Name); break;

                case "department_desc":
                    query = query.OrderByDescending(i => i.Department.Name); break;

                case "email_asc":
                    query = query.OrderBy(i => i.Email); break;

                case "email_desc":
                    query = query.OrderByDescending(i => i.Email); break;
                default:
                    query = query.OrderBy(i => i.FirstName); break;
            }

            var instructors = await query.Select(i => i.ConvertToViewModel()).ToListAsync();

            return instructors;
        }

        public async Task<InstructorActionViewModel> Update(InstructorActionViewModel instructor)
        {
            if (!EntityExist(instructor.Id))
            {
                throw new EntityNotFoundException();
            }

            var entity = _context.Instructors.Update(instructor.ConvertToInstructor());
            await _context.SaveChangesAsync();

            return entity.Entity.ConvertToInstructorAction();
        }
        private bool EntityExist(int id)
        {
            return _context.Instructors.Any(i => i.Id == id);
        }
    }
}
