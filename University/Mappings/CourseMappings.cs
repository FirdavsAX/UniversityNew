using Microsoft.CodeAnalysis.Elfie.Extensions;
using University.Entities;
using University.Models.CourseViewModel;
using UniversityWeb.Entities;

namespace University.Mappings
{
    public static class CourseMappings
    {
        public static Course ConvertToCourse(this CourseDisplayViewModel course)
        {
            return new Course()
            {
                Id = course.Id,
                Name = course.Name,
                Hours = course.Hours,
                Price = course.Price,
                CategoryId = course.CategoryId,
            };
        }
        public static CourseDisplayViewModel ConvertToViewModel(this Course course)
        {
            var viewModel = new CourseDisplayViewModel()
            {
                Id = course.Id,
                Name = course.Name,
                Hours = course.Hours,
                Price = course.Price,
                CategoryId = course.CategoryId,
                CourseAssignments = course.CourseAssignments
            };

            if (course.Category is not null)
            {
                viewModel.Category = course.Category.Name;
            }

            return viewModel;
        }

    }

}
