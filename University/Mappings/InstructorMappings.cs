using University.Models.InstructorViewModels;
using UniversityWeb.Entities;

namespace University.Mappings
{
    public static class InstructorMappings
    {
        public static InstructorDisplayViewModel ConvertToViewModel(this Instructor instructor)
        {
            return new InstructorDisplayViewModel()
            {
                Id = instructor.Id,
                FullName = instructor.FirstName + " " + instructor.LastName,
                Email = instructor.Email,
                DepartmentId = instructor.DepartmentId,
                Department = instructor.Department.Name
            };
        }
        public static Instructor ConvertToInstructor(this InstructorActionViewModel instructorAction)
        {
            return new Instructor()
            {
                Id = instructorAction.Id,
                FirstName = instructorAction.FirstName,
                LastName = instructorAction.LastName,
                Email = instructorAction.Email,
                DepartmentId = instructorAction.DepartmentId
            };
        }
        public static InstructorActionViewModel ConvertToInstructorAction(this Instructor instructor)
        {
            return new InstructorActionViewModel()
            {
                Id = instructor.Id,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Email = instructor.Email,
                DepartmentId = instructor.DepartmentId,
                Department = instructor.Department.Name

            };
        }
    }
}
