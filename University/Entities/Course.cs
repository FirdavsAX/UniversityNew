using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace UniversityWeb.Entities
{
    [Table(nameof(Course))]
    public class Course
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required,MaxLength(255),MinLength(4)]
        public string Name { get; set; }

        [Required,DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public int Hours { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; }

        public virtual ICollection<CourseAssignment> CourseAssignments { get; set; }
        public Course()
        {
            CourseAssignments = new List<CourseAssignment>();
        }
    }
}
