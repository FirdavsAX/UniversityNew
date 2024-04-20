using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using University.Entities;
using UniversityWeb.Entities;

namespace University.Models.CourseViewModel
{
    public class CourseDisplayViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int Hours { get; set; }
        public IFormFile? Image{ get; set; }

        [DisplayName("Category ")]
        public int CategoryId { get; set; }
        public string? Category { get; set; }
        public virtual ICollection<CourseAssignment> CourseAssignments { get; set; }
        public CourseDisplayViewModel()
        {
            CourseAssignments = new List<CourseAssignment>();
        }
    }
}
