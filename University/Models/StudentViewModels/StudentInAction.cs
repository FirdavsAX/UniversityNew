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

    }
}
