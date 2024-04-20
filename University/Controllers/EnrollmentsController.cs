 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Data;
using University.Models.EnrollmentViewModels;
using University.Services.CourseAssignmentServices;
using University.Services.EnrollmentServices;
using University.Services.StudentServices;
using UniversityWeb.Entities;

namespace University.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly ICourseAssignmentService _courseAssignmentService;
        private readonly IStudentService _studentService;
        public EnrollmentsController(IEnrollmentService enrollment,ICourseAssignmentService _courseAssignmentService,IStudentService _studentService)
        {
            _enrollmentService = enrollment;
            this._courseAssignmentService = _courseAssignmentService;
            this._studentService = _studentService;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var enrollments = await _enrollmentService.GetEnrollments();
            
            return View(enrollments);
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var enrollment = await _enrollmentService.GetById(id);
            
            if (enrollment is null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CourseAssignments"] = new SelectList(await _courseAssignmentService.GetCourseAssignmentsAsync(), "Id", "Definition");
            ViewData["Students"] = new SelectList(await _studentService.GetStudents(), "Id", "FullName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnrollmentViewModel enrollment)
        {
            if (ModelState.IsValid)
            {
                await _enrollmentService.CreateAsync(enrollment);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseAssignments"] = new SelectList(await _courseAssignmentService.GetCourseAssignmentsAsync(), "Id", "Definition", enrollment.CourseAssignmentId);
            ViewData["Students"] = new SelectList(await _studentService.GetStudents(), "Id", "FullName",enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var enrollment = await _enrollmentService.GetById(id);

            ViewData["CourseAssignments"] = new SelectList(await _courseAssignmentService.GetCourseAssignmentsAsync(), "Id", "Definition", enrollment.CourseAssignmentId);
            ViewData["Students"] = new SelectList(await _studentService.GetStudents(), "Id", "FullName", enrollment.StudentId);

            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,EnrollmentViewModel enrollment)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _enrollmentService.UpdateAsync(enrollment);
                }
                catch (Exception ex)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseAssignments"] = new SelectList(await _courseAssignmentService.GetCourseAssignmentsAsync(), "Id", "Definition", enrollment.CourseAssignmentId);
            ViewData["Students"] = new SelectList(await _studentService.GetStudents(), "Id", "FullName", enrollment.StudentId);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _enrollmentService.GetById(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _enrollmentService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
