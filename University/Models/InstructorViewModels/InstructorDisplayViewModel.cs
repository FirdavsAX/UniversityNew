
using System.ComponentModel;
using UniversityWeb.Entities;
namespace University.Models.InstructorViewModels
{
    public class InstructorDisplayViewModel
    {
        public int Id { get; set; }
        [DisplayName("Full name")]
        public string FullName { get; set; }
        public string Email { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
       
    }
}
