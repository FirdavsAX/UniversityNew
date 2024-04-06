using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models.InstructionViewModels;
using UniversityWeb.Entities;

namespace University.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly UniversityDbContext _context;
        public InstructorsController(UniversityDbContext _context)
        {
            this._context = _context;
        }

        public async Task<IActionResult> Index()
        {
            var instructors = _context.Instructors.Include(i => i.Department).Select(i => new InstructorDisplayViewModel()
            {
                Id = i.Id,
                FullName = i.FirstName + "  " + i.LastName,
                Email = i.Email,
                DepartmentId = i.DepartmentId,
                Department = i.Department.Name
            })
                .AsQueryable();
            
            
            return View(await instructors.ToListAsync());
        }
            
        public async Task<IActionResult> Create()
        {
            var deparmtents = await _context.Departments.ToListAsync();

            ViewBag.Departments = new SelectList(deparmtents, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InstructorActionViewModel instructorAction)
        {
            if (!ModelState.IsValid)
            {
                var deparmtents = await _context.Departments.ToListAsync();
                ViewBag.Departments = new SelectList(deparmtents, "Id", "Name");
                return View(instructorAction);

            }
            var instructor = new Instructor()
            {
                FirstName = instructorAction.FirstName,
                LastName = instructorAction.LastName,
                Email = instructorAction.Email,
                DepartmentId = instructorAction.DepartmentId
            };

            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        private bool InstructorExist(int Id)
        {
            return _context.Instructors.Any(i => i.Id == Id);
        }
    }
}
