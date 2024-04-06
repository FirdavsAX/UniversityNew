using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Data;
using UniversityWeb.Entities;
using University.Models.DepartmentViewModels;
using System.Data;

namespace University.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly UniversityDbContext _context;
        public DepartmentsController(UniversityDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<IActionResult> Index(string? searchString)
        {
            
            var departments = _context.Departments.AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                departments.Where(d => d.Name.Contains(searchString));
            }
            var departmentViews = await departments.Select(x => new DepartmentViewModel()
            {
                Id = x.Id,
                Name = x.Name 
            }).ToListAsync();

            return View(departmentViews);
        }

        public IActionResult Create()
        {
            return View();
        }
         
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentViewModel);
            }
            Department department = new Department()
            {
                Name = departmentViewModel.Name
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult>Details(int id)
        {
            var department = await _context.Departments.Select(d => new DepartmentViewModel()
            {
                Name = d.Name,
                Id = d.Id
            }).FirstOrDefaultAsync(d =>d.Id == id);
            
            if(department is null)
            {
                return NotFound();
            }

            return View(department);
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(!id.HasValue || id == 0)
            {
                return NotFound();
            }

            var department = _context.Departments.FirstOrDefault(x => x.Id == id);
            var departmentView = new DepartmentViewModel()
            {
                Name = department.Name,
                Id = department.Id
            };

            return View(departmentView);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int Id,DepartmentViewModel departmentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentViewModel);
            }


            if(Id == 0 || Id != departmentViewModel.Id)
            {
                return NotFound();
            }
            
            
            var department = new Department()
            {
                Id = departmentViewModel.Id,
                Name = departmentViewModel.Name
            };

            try
            {
                _context.Departments.Update(department);
                await _context.SaveChangesAsync();
            }
            catch (DBConcurrencyException ex)
            {
                if (DepartmentExist(department.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null|| id == 0 || !DepartmentExist(id ?? 0))
            {
                return NotFound();
            }
            var departmentVeiewModel = await _context.Departments.Select(d => new DepartmentViewModel()
            {
                Name = d.Name,
                Id = d.Id
            }).FirstOrDefaultAsync(d => d.Id == id);

            if(departmentVeiewModel is null)
            {
                return NotFound();
            }

            return View(departmentVeiewModel);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if(department is null)
            {
                return NotFound();
            }

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        private bool DepartmentExist(int Id)
        {
            return _context.Departments.Any(d => d.Id == Id);
        }
    }
}
