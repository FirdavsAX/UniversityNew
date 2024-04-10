using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using UniversityWeb.Entities;

namespace University.Models.StudentViewModels
{
    public class StudentInAction
    {
        public int Id { get; set; }
        [Required, MaxLength(100), MinLength(2), DisplayName("First name")]
        public string FirstName { get; set; }

        [MaxLength(100), MinLength(2), DisplayName("Last name")]
        public string? LastName { get; set; }

        [DataType(DataType.EmailAddress), Required, MaxLength(200), MinLength(5)]
        public string Email { get; set; }

        public static Student ConvertToStudent(StudentInAction studentAction)
        {
            Student student;
            try
            {
                student = new Student()
                {
                    Id = studentAction.Id,
                    FirstName = studentAction.FirstName,
                    LastName = studentAction.LastName,
                    Email = studentAction.Email,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return student;
        }
        public static StudentInAction ConvertToStudentInAction(Student student)
        {
            StudentInAction studentInAction;
            try
            {
                studentInAction = new StudentInAction()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return studentInAction;
        }
    }
}
