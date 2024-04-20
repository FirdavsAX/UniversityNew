using University.Models.InstructorViewModels;
using University.Models.StudentViewModels;
using UniversityWeb.Entities;

namespace University.Mappings
{
    public static class StudentMapping
    {

        public static StudentDisplay ConvertToViewModel(this Student student)
        {
            return new StudentDisplay()
            {
                Id = student.Id,
                FullName = student.FirstName + " " + student.LastName,
                Email = student.Email,
            };
        }
        public static Student ConvertToStudent(this StudentInAction studentInAction)
        {
            return new Student()
            {
                Id = studentInAction.Id,
                FirstName = studentInAction.FirstName,
                LastName = studentInAction.LastName,
                Email = studentInAction.Email,
            };
        }
        public static StudentInAction ConvertToStudentAction(this Student student)
        {
            return new StudentInAction()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
            };
        }
    }
}
