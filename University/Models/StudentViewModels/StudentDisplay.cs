
using System.ComponentModel;
using UniversityWeb.Entities;

namespace University.Models.StudentViewModels
{
    public class StudentDisplay
    {
        public int Id { get; set; }

        [DisplayName("Full name")]
        public string FullName { get; set; }

        public string Email { get; set; }

        public List<Enrollment> Enrollments { get; set; }
        public StudentDisplay()
        {
            Enrollments = new List<Enrollment>();
        }
    }
}
