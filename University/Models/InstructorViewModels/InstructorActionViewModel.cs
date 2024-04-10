using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UniversityWeb.Entities;

namespace University.Models.InstructorViewModels
{
    public class InstructorActionViewModel
    {
        public int Id{ get; set; }
        [Required, MaxLength(100), MinLength(2), DisplayName("First name")]
        public string FirstName { get; set; }

        [MaxLength(100), MinLength(2), DisplayName("Last name")]
        public string? LastName { get; set; }

        [DataType(DataType.EmailAddress),Required, MaxLength(200), MinLength(5)]
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public string? Department { get; set; }

        public static Instructor ConvertToInstructor(InstructorActionViewModel instructorActionViewModel)
        {
            Instructor instructor;
            try
            {
                instructor = new Instructor()
                {
                    Id = instructorActionViewModel.Id,
                    FirstName = instructorActionViewModel.FirstName,
                    LastName = instructorActionViewModel.LastName,
                    Email = instructorActionViewModel.Email,
                    DepartmentId = instructorActionViewModel.DepartmentId
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
            return instructor;
        }
        public static InstructorActionViewModel ConvertToInstructorActionViewModel(Instructor instructor)
        {
            InstructorActionViewModel instructorActionViewModel;
            try
            {
                instructorActionViewModel = new InstructorActionViewModel()
                {
                    Id = instructor.Id,
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName,
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
            return instructorActionViewModel;
        }
    }
}
