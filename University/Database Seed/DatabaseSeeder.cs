using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using University.Data;
using UniversityWeb.Entities;

namespace University.Database_Seed
{
    public class DatabaseSeeder
    {
        public static async Task SeedDatabase(UniversityDbContext context)
        {
            await CreateStudents(context);
            await CreateInstructors(context);
            await CreateCourseAssignments(context);
            await CreateEnrollments(context);
        }

        private static async Task CreateStudents(UniversityDbContext context)
        {
            if (context.Students.Any())
            {
                return;
            }
            for (int i = 0; i < 300; i++)
            {
                Faker faker = new Faker(); // Create a new Faker instance for each student
                Student student = new Student()
                {
                    FirstName = faker.Person.FirstName,
                    LastName = faker.Person.LastName,
                    Email = faker.Person.Email,
                };
                context.Students.Add(student);
            }
            context.SaveChanges();
        }
        private static async Task CreateInstructors(UniversityDbContext context)
        {
            if (context.Instructors.Any())
            {
                return;
            }
            var departments = await context.Departments.Select(c => c.Id).ToListAsync();

            for (int i = 0; i < 100; i++)
            {
                Faker faker = new Faker(); // Create a new Faker instance for each student
                Instructor instructor = new Instructor()
                {
                    FirstName = faker.Person.FirstName,
                    LastName = faker.Person.LastName,
                    Email = faker.Person.Email,
                    DepartmentId = faker.Random.ArrayElement(departments.ToArray()),
                };

                context.Instructors.Add(instructor);
            }
            await context.SaveChangesAsync();
        }

        private static async Task CreateCourseAssignments(UniversityDbContext context)
        {
            if (context.CourseAssignments.Any())
            {
                return;
            }
            var instructors = await context.Instructors.Select(c => c.Id).ToListAsync();
            var courses = await context.Courses.Select(c => c.Id).ToListAsync();
            string[] roomLetters = ["A", "B","C","D","E","F","G","H","I","J"];

            for (int i = 0; i < instructors.Count ; i++)
            {
                Faker faker = new(); 
                CourseAssignment courseAssignment = new CourseAssignment()
                {
                    CourseId = faker.Random.ArrayElement(courses.ToArray()),
                    InstructorId = faker.Random.ArrayElement(instructors.ToArray()),
                    Room = faker.Random.ArrayElement(roomLetters) + "-" + faker.Random.Int(100,699) 
                };
                context.CourseAssignments.Add(courseAssignment);
            }
            await context.SaveChangesAsync();
        }
        private static async Task CreateEnrollments(UniversityDbContext context)
        {
            if (context.Enrollments.Any())
            {
                return;
            }
            var courseAssignments = await context.CourseAssignments.Select(c => c.Id).ToListAsync();
            var students = await context.Students.Select(c => c.Id).ToListAsync();

            for (int i = 0; i < students.Count; i++)
            {
                Faker faker = new Faker(); // Create a new Faker instance for each student
                Enrollment enrollment = new Enrollment()
                {
                    CourseAssignmentId = faker.Random.ArrayElement(courseAssignments.ToArray()),
                    StudentId = faker.Random.ArrayElement(students.ToArray()),
                };

                context.Enrollments.Add(enrollment);
            }
            await context.SaveChangesAsync();
        }

    }
}
