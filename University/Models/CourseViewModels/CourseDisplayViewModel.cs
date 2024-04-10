using UniversityWeb.Entities;
namespace University.Models.CourseViewModels
{
    public class CourseDisplayViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public decimal Price { get; set; }
        public int Hours { get; set; }
        public int InstructorId { get; set; }
        public string Instructor { get; set; }
        public ICollection<Student> Students { get; set; }
        public CourseDisplayViewModel()
        {
            Students = new List<Student>();
        }
    }
}
