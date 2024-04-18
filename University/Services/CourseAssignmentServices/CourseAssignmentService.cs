using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Exceptions;
using University.Mappings;
using University.Models.CourseAssignmentViewModels;

namespace University.Services.CourseAssignmentServices
{
    public class CourseAssignmentService : ICourseAssignmentService
    {
        private readonly UniversityDbContext _context;
        public CourseAssignmentService(UniversityDbContext _context)
        {
            this._context = _context;
        }
        public async Task<CourseAssignmentDisplayViewModel> CreateAsync(CourseAssignmentDisplayViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException();
            }
            var entity = _context.CourseAssignments.Add(viewModel.ConvertToCourseAssignment());
            await _context.SaveChangesAsync();

            entity.Entity.Course = _context.Courses.FirstOrDefault(c => c.Id == viewModel.CourseId);
            entity.Entity.Instructor = _context.Instructors.FirstOrDefault(c => c.Id == viewModel.InstructorId);
            return entity.Entity.ConvertToViewModel();
        }

        public async Task DeleteAsync(int id)
        {
            var courseAssignment = await _context.CourseAssignments.FirstOrDefaultAsync(c => c.Id == id);

            if(courseAssignment == null) 
            {
                throw new CourseAssignmentNotFoundException($"Course assignment {id} is not found!");
            }

            _context.CourseAssignments.Remove(courseAssignment);
            await _context.SaveChangesAsync();
        }

        public async Task<CourseAssignmentDisplayViewModel> GetByIdAsync(int id)
        {
            var courseAssignment = await _context.CourseAssignments.FirstOrDefaultAsync(c => c.Id == id);

            return courseAssignment == null
                ? throw new CourseAssignmentNotFoundException($"Course assignment {id} is not found!")
                : courseAssignment.ConvertToViewModel();
        }

        public async Task<ICollection<CourseAssignmentDisplayViewModel>> GetCourseAssignmentsAsync(int? instructorId, int? courseId ,string? searchString = "", string? sortOrder = "room_asc")
        {
            var query = _context.CourseAssignments.Include(c => c.Instructor).Include(c => c.Course).AsQueryable();

            if(searchString != null)
            {
                query = query.Where(c => c.Room.Contains(searchString));
            }
            if( instructorId.HasValue && instructorId != 0)
            {
                query = query.Where(c => c.InstructorId == instructorId);
            }
            if (courseId.HasValue && courseId != 0)
            {
                query = query.Where(c => c.CourseId == courseId);
            }

            _ = sortOrder switch
            {
                "room_desc" => query = query.OrderByDescending(c => c.Room),
                "instructor_asc" => query.OrderBy(c => c.Instructor.FirstName).ThenBy(c => c.Instructor.LastName),
                "instructor_desc" => query.OrderByDescending(c => c.Instructor.FirstName).ThenByDescending(c => c.Instructor.LastName),
                "course_asc" => query = query.OrderBy(c => c.Course.Name),
                "course_desc" => query = query.OrderByDescending(c => c.Course.Name),
                _ => query = query.OrderBy(c => c.Room)
            };

            return await query
                .Select(c=>c.ConvertToViewModel())
                .ToListAsync();
        }

        public async Task<CourseAssignmentDisplayViewModel> UpdateAsync(CourseAssignmentDisplayViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            var entity = await _context.CourseAssignments.FirstOrDefaultAsync(c =>c.Id == viewModel.Id);
            
            if(entity == null) 
            {
                throw new CourseAssignmentNotFoundException(nameof(viewModel));
            }

            _context.CourseAssignments.Attach(entity);
            await _context.SaveChangesAsync();

            entity.Course = _context.Courses.FirstOrDefault(c => c.Id == viewModel.CourseId);
            entity.Instructor = _context.Instructors.FirstOrDefault(c => c.Id == viewModel.InstructorId);

            return entity.ConvertToViewModel();
        }
    }
}
