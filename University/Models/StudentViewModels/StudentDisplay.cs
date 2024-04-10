
using System.ComponentModel;
using UniversityWeb.Entities;

namespace University.Models.StudentViewModels
{
    public class StudentDisplay
    {
        public int Id { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string? LastName { get; set; }
        public string Email { get; set; }

        public List<Enrollment> Enrollments { get; set; }
        public static StudentDisplay ConvertToStudentInAction(Student student)
        {
            StudentDisplay studentDisplay;
            try
            {
                studentDisplay = new StudentDisplay()
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
            return studentDisplay;
        }
    }
}
