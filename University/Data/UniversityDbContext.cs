using Microsoft.EntityFrameworkCore;
using UniversityWeb.Entities;
using University.Models.InstructionViewModels;
using University.Models.DepartmentViewModels;

namespace University.Data
{
    public class UniversityDbContext:DbContext
    {
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Instructor> Instructors{ get; set; }
        public virtual DbSet<Course> Courses  { get; set; }
        public virtual DbSet<CourseAssignment> CourseAssignments{ get; set; }
        public virtual DbSet<Student>Students{ get; set; }
        public virtual DbSet<Enrollment>Enrollments{ get; set; }

        public UniversityDbContext(DbContextOptions<UniversityDbContext>options ):base(options)
        {
            
        }
        
    }
}
