using Microsoft.AspNetCore.Mvc;
using University.Models.StudentViewModels;
using University.Services.StudentServices;

namespace University.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService _studentService)
        {
            this._studentService = _studentService;
        }

        public async Task<IActionResult> Index(string? searchString, string? sortOrder)
        {

            ViewData["NameSort"] = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewData["EmailSort"] = sortOrder == "email_asc" ? "email_desc" : "email_asc";

            var students = await _studentService.GetInstructors(searchString, sortOrder);

            ViewBag.SearchedString = searchString ?? "";
            return View(students);
        }
        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetById(id);

            if (student is null)
            {
                return NotFound();
            }

            return View(student);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentInAction student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            _= await _studentService.CreateAsync(student);
            return RedirectToAction(nameof(Index));
        }

        //GET
        public async Task<IActionResult> Edit(int id)
        {
            var student = (await _studentService.GetByIdToAction(id));

            if (student is null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentInAction student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            var studentUpdated = await _studentService.Update(student);

            return RedirectToAction(nameof(Details), new { studentUpdated.Id });
        }
        //Get
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetById(id);

            if (student is null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(StudentInAction student)
        {
            if (student is null)
            {
                return NotFound();
            }

            await _studentService.Delete(student.Id);
            return RedirectToAction(nameof(Index));
        }
    }

}

