
using UniversityWeb.Entities;
namespace University.Models.InstructorViewModels
{
    public class InstructorDisplayViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public static InstructorDisplayViewModel ConvertToInstructorDisplayViewModel(Instructor instructor)
        {
            InstructorDisplayViewModel instructorDisplayViewModel;
            try
            {
                instructorDisplayViewModel = new InstructorDisplayViewModel()
                {
                    Id = instructor.Id,
                    FullName = instructor.FirstName + "  " + instructor.LastName,
                    Email = instructor.Email,
                    DepartmentId = instructor.DepartmentId,
                    Department = instructor.Department.Name
                };
            }
            catch (ArgumentNullException aex)
            {
                throw aex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return instructorDisplayViewModel;
        }
    }
}
