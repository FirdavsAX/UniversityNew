using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityWeb.Entities
{
    [Table(nameof(Student))]
    public class Student
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(100), MinLength(2), DisplayName("First name")]
        public string FirstName { get; set; }

        [MaxLength(100), MinLength(2), DisplayName("Last name")]
        public string? LastName { get; set; }

        [Required, MaxLength(200), MinLength(5)]
        public string Email { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public Student()
        {
            Enrollments = new List<Enrollment>();
        }
    }
}
