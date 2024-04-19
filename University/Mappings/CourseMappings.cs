using Bogus.DataSets;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using System.Diagnostics.Tracing;
using System.Drawing;
using University.Entities;
using University.Models.CourseViewModel;
using UniversityWeb.Entities;

namespace University.Mappings
{
    public static class CourseMappings
    {
        public static Course ConvertToCourse(this CourseDisplayViewModel course)
        {
            var entity =  new Course()
            {
                Id = course.Id,
                Name = course.Name,
                Hours = course.Hours,
                Price = course.Price,
                CategoryId = course.CategoryId,
            };
            
            if(course.Image != null && course.Image.Length > 0)
            {
                byte[] imgBytes;

                using(var stream = new MemoryStream())
                {
                    course.Image.CopyTo(stream);
                    imgBytes = stream.ToArray();
                }
                entity.Image = imgBytes;
            }

            return entity;
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

            if(course.Image != null && course.Image.Length > 0) 
            {
                var imageData = course.Image;
                viewModel.Image = new FormFile(new MemoryStream(imageData), 0, imageData.Length, "Image", "image.jpg");
            }

            return viewModel;
        }

    }

}
