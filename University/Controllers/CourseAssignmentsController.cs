using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models.CourseAssignmentViewModels;
using University.Services;
using University.Services.CourseAssignmentServices;
using University.Services.CourseSerives;
using UniversityWeb.Entities;

namespace University.Controllers
{
    public class CourseAssignmentsController : Controller
    {
        private readonly ICourseAssignmentService _courseAssignmentService;
        private readonly IInstructorService _instructorService;
        private readonly ICourseService _courseService;
        public CourseAssignmentsController(ICourseAssignmentService courseAssignmentService,IInstructorService instructorService,ICourseService courseService)
        {
            _courseAssignmentService = courseAssignmentService;
            _instructorService = instructorService;
            _courseService = courseService;
        }

        // GET: CourseAssignments
        public async Task<IActionResult> Index(string? searchString,string sortOrder,int? instructorId , int? courseId)
        {
            ViewData["RoomSort"] = sortOrder == "room_asc" ? "room_desc" : "room_asc";
            ViewData["InstructorSort"] = sortOrder == "instructor_asc" ? "instrcutor_desc" : "instructor_asc";
            ViewData["CourseSort"] = sortOrder == "course_asc" ? "course_desc" : "course_asc";

            var courseAssignments = await _courseAssignmentService.GetCourseAssignmentsAsync(instructorId, courseId,searchString, sortOrder);
            
            ViewBag.SearchString = searchString;
            ViewBag.Instructors = new SelectList(await _instructorService.GetInstructors(),"Id","FullName",instructorId ?? 0);
            ViewBag.Courses = new SelectList(await _courseService.GetCoursesAsync(), "Id", "Name" , courseId ?? 0);
            return View(courseAssignments);
        }

        // GET: CourseAssignments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var courseAssignment = await _courseAssignmentService.GetByIdAsync(id);

            if (courseAssignment == null)
            {
                return NotFound();
            }

            return View(courseAssignment);
        }

        // GET: CourseAssignments/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Courses"] = new SelectList(await _courseService.GetCoursesAsync(), "Id", "Name");
            ViewData["Instructors"] = new SelectList(await _instructorService.GetInstructors(), "Id", "FullName");

            return View();
        }

        // POST: CourseAssignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseAssignmentDisplayViewModel courseAssignment)
        {
            if (ModelState.IsValid)
            {
                await _courseAssignmentService.CreateAsync(courseAssignment);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Courses"] = new SelectList(await _courseService.GetCoursesAsync(), "Id", "Name");
            ViewData["Instructors"] = new SelectList(await _instructorService.GetInstructors(), "Id", "FullName");
            return View(courseAssignment);
        }

        // GET: CourseAssignments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var courseAssignment = await _courseAssignmentService.GetByIdAsync(id);

            if (courseAssignment == null)
            {
                return NotFound();
            }
            ViewData["Courses"] = new SelectList(await _courseService.GetCoursesAsync(), "Id", "Name");
            ViewData["Instructors"] = new SelectList(await _instructorService.GetInstructors(), "Id", "FullName");
            return View(courseAssignment);
        }

        // POST: CourseAssignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseAssignmentDisplayViewModel courseAssignment)
        {
            if (id != courseAssignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _ = await _courseAssignmentService.UpdateAsync(courseAssignment);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Courses"] = new SelectList(await _courseService.GetCoursesAsync(), "Id", "Name");
            ViewData["Instructors"] = new SelectList(await _instructorService.GetInstructors(), "Id", "FullName");
            return View(courseAssignment);
        }

        // GET: CourseAssignments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var courseAssignment = await _courseAssignmentService.GetByIdAsync(id);
            if (courseAssignment == null)
            {
                return NotFound();
            }

            return View(courseAssignment);
        }

        // POST: CourseAssignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id != 0)
            {
                await _courseAssignmentService.DeleteAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
