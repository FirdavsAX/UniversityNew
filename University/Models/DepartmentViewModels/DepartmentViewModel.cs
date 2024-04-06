
using UniversityWeb.Entities;

namespace University.Models.DepartmentViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public DepartmentViewModel()
        {
            Instructors = new List<Instructor>();
        }
    }
}
