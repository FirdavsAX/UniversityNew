using AspNetCore;
using Bogus;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Xml.Serialization;
using University.Data;
using University.Entities;
using UniversityWeb.Entities;

namespace University.Database_Seed
{
    public class DatabaseSeeder
    {
        public static void SeedDatabase(UniversityDbContext context)
        {
            Createstudents(context);
        }

        private static void Createstudents(UniversityDbContext context)
        {
            Faker faker = new Faker();
            for (int i = 0; i < 10; i++) 
            {
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
    }
}
