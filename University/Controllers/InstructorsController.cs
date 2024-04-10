using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Mappings;
using University.Models.InstructorViewModels;
using University.Services;
using UniversityWeb.Entities;

namespace University.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly IInstructorService _instructorService;
        private readonly UniversityDbContext _context;
        public InstructorsController(UniversityDbContext _context,IInstructorService service)
        {
            this._instructorService = service;
            this._context = _context;
        }

        public async Task<IActionResult> Index(int? departmentId,string? searchString,string? sortOrder)
        {
            
            ViewData["NameSort"] = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewData["DepartmentSort"] = sortOrder == "department_asc" ? "department_desc" : "department_asc";
            ViewData["EmailSort"] = sortOrder == "email_asc" ? "email_desc" : "email_asc";

            var instructors = await _instructorService.GetInstructors(departmentId, searchString, sortOrder);

            ViewBag.SearchedString = searchString ?? "";
            ViewBag.Departments = new SelectList(await _context.Departments.ToListAsync(), "Id", "Name",departmentId ?? 0);
            
            return View(instructors);
        }
        public async Task<IActionResult> Details(int id)
        {
            var instructor = _instructorService.GetById(id);

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
            
            _= _instructorService.Create(instructorAction);
            return RedirectToAction(nameof(Index));
        }

        //GET
        public async Task<IActionResult> Edit(int id) 
        {
            var instructor = _instructorService.GetById(id);

            if(instructor is null)
            {
                return NotFound();
            }

            ViewBag.Departments = new SelectList( _context.Departments.ToList(), "Id", "Name");
                //_context.Departments.FirstOrDefaultAsync(d => d.Id == instructorInAction.DepartmentId));

            return View(instructor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InstructorActionViewModel instructorAction)
        {
            if(!ModelState.IsValid)
            {
                return View(instructorAction);
            }
            
            var instructor = _instructorService.Update(instructorAction);
             
            return RedirectToAction(nameof(Details) + "/" + instructor.Id);
        }
        //Get
        public async Task<IActionResult> Delete(int id)
        {
            var instructor = _instructorService.GetById(id);
            
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
            
            _instructorService.Delete(instructor.Id);
            return RedirectToAction(nameof(Index));
        }
    }
    
}
