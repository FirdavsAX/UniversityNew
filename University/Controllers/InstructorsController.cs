using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Mappings;
using University.Models.InstructorViewModels;
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

        public async Task<IActionResult> Index(int? departmentId,string? searchString,string? sortOrder)
        {

            var query = _context.Instructors.Include(i => i.Department).AsQueryable();

            ViewData["NameSort"] = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewData["DepartmentSort"] = sortOrder == "department_asc" ? "department_desc" : "department_asc";
            ViewData["EmailSort"] = sortOrder == "email_asc" ? "email_desc" : "email_asc";

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(i => i.FirstName.Contains(searchString) || i.LastName.Contains(searchString)
                    || i.Email.Contains(searchString) || i.Department.Name.Contains(searchString));
            }
            if (departmentId.HasValue)
            {
                query = query.Where(i => i.DepartmentId == departmentId);
            }

            switch (sortOrder)
            {
                case "name_asc":  
                    query = query.OrderBy(i => i.FirstName); break;

                case "name_desc": 
                    query = query.OrderByDescending(i => i.FirstName); break;

                case "department_asc":
                    query = query.OrderBy(i => i.Department.Name); break;

                case "department_desc":
                    query = query.OrderByDescending(i => i.Department.Name); break;

                case "email_asc": 
                    query = query.OrderBy(i => i.Email); break;

                case "email_desc":
                    query = query.OrderByDescending(i => i.Email); break;
            }

            var instructors = await query.Select(i => i.ConvertToViewModel()).ToListAsync();

            ViewBag.SearchedString = searchString ?? "";
            ViewBag.Departments = new SelectList(await _context.Departments.ToListAsync(), "Id", "Name",departmentId ?? 0);
            
            return View(instructors);
        }
        public async Task<IActionResult> Details(int id)
        {
            var instructor = _context.Instructors.Include(i => i.Department)
                .Select(i => i.ConvertToViewModel())
                .FirstOrDefault(i => i.Id == id);

            if(instructor is null)
            {
                return NotFound();
            }
            
            return View(instructor);
        }
            
        public async Task<IActionResult> Create()
        {
            var departments = await _context.Departments.ToListAsync();

            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(InstructorActionViewModel instructorAction)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _context.Departments.ToListAsync();
                ViewBag.Departments = new SelectList(departments, "Id", "Name");
                return View(instructorAction);
            }
            
            _context.Instructors.Add(instructorAction.ConvertToInstructor());
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //GET
        public async Task<IActionResult> Edit(int id) 
        {
            var instructor = await _context.Instructors.Include(i => i.Department)
                .FirstOrDefaultAsync(i => i.Id == id);

            if(instructor is null)
            {
                return NotFound();
            }

            ViewBag.Departments = new SelectList( _context.Departments.ToList(), "Id", "Name");
                //_context.Departments.FirstOrDefaultAsync(d => d.Id == instructorInAction.DepartmentId));

            return View(instructor.ConvertToInstructorAction());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InstructorActionViewModel instructorAction)
        {
            if(!ModelState.IsValid)
            {
                return View(instructorAction);
            }
            
            if (!InstructorExist(instructorAction.Id))
            {
                return NotFound();
            }

            _context.Instructors.Update(instructorAction.ConvertToInstructor());
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        //Get
        public async Task<IActionResult> Delete(int id)
        {
            var instructor = await _context.Instructors
                .Include(i => i.Department)
                .Select(i => i.ConvertToViewModel())
                .FirstOrDefaultAsync(i => i.Id == id);
            
            if (instructor is null)
            {
                return NotFound();
            }
            
            return View(instructor);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(InstructorActionViewModel instructor)
        {
            if (instructor is null)
            {
                return NotFound();
            }
            
            if (!InstructorExist(instructor.Id))
            {
                return NotFound();
            }

            _context.Instructors.Remove(instructor.ConvertToInstructor());
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool InstructorExist(int Id)
        {
            return _context.Instructors.Any(i => i.Id == Id);
        }
    }
    
}
