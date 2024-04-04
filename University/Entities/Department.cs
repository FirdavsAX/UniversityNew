using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWeb.Entities
{
    [Table(nameof(Department))]
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Instructor> Instructors { get; set; }
        public Department()
        {
            Instructors = new List<Instructor>();
        }
    }
}
