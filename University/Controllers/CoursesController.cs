using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Entities;
using University.Models.CourseViewModel;
using University.Services.CategoryServices;
using University.Services.CourseSerives;
using UniversityWeb.Entities;

namespace University.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;

        public CoursesController(ICourseService _courseService,ICategoryService _categoryService)
        {
            this._courseService = _courseService;
            this._categoryService = _categoryService; 
        }

        // GET: Courses
        public async Task<IActionResult> Index(string? searchString, string? sortOrder,int? categoryId)
        {
            ViewData["NameSort"] = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewData["PriceSort"] = sortOrder == "price_asc" ? "price_desc" : "price_asc";
            ViewData["HoursSort"] = sortOrder == "hours_asc" ? "hours_desc" : "hours_asc";
            ViewData["CategorySort"] = sortOrder == "category_asc" ? "category_desc" : "category_asc";

            var courses = await _courseService.GetCoursesAsync(searchString, categoryId, sortOrder);

            ViewBag.SearchString = searchString ?? "";
            ViewBag.Categories = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name", categoryId ?? 0);

            return View(courses);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
           var course = await _courseService.GetByIdAsync(id);
            
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseDisplayViewModel course)
        {
            if (ModelState.IsValid)
            {
                await _courseService.CreateAsync    (course);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetByIdAsync(id);

            if (course == null)
            {
                return NotFound();
            }
            
            ViewBag.Categories = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,CourseDisplayViewModel course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _courseService.UpdateAsync(course);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id != 0)
            {
                await _courseService.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
