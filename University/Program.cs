using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Services;
using University.Services.CategoryServices;
using University.Services.CourseAssignmentServices;
using University.Services.CourseSerives;
using University.Services.StudentServices;

namespace University
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<UniversityDbContext>(options => options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IInstructorService, InstructorService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ICourseService,CourseService>();
            builder.Services.AddScoped<ICourseAssignmentService, CourseAssignmentService>();
            builder.Services.AddScoped<IStudentService,StudentService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
