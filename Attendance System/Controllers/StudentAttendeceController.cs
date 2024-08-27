using Attendance_System.Models;
using Attendance_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Attendance_System.Controllers
{
    public class StudentAttendeceController : Controller
    {
         IStudentAttendeceService _studentAttendeceService;
         IStudentService studentService;   

        public StudentAttendeceController(IStudentAttendeceService studentAttendeceService,IStudentService _studentService)
        {
            _studentAttendeceService = studentAttendeceService;
            studentService = _studentService;
        }

        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var model = studentService.GetById(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.std = model;
            return View();
        }

        [HttpPost]
        public IActionResult Index(StudentAttendece model)
        {
            if (ModelState.IsValid)
            {
                model.Saturday = false;
                model.Sunday = false;
                model.Monday = false;
                model.Tuesday = false;
                model.Wednesday = false;
                model.Thursday = false;
                model.Friday = false;

                switch (model.SelectedDay)
                {
                    case "Saturday":
                        model.Saturday = true;
                        break;
                    case "Sunday":
                        model.Sunday = true;
                        break;
                    case "Monday":
                        model.Monday = true;
                        break;
                    case "Tuesday":
                        model.Tuesday = true;
                        break;
                    case "Wednesday":
                        model.Wednesday = true;
                        break;
                    case "Thursday":
                        model.Thursday = true;
                        break;
                    case "Friday":
                        model.Friday = true;
                        break;
                }

                _studentAttendeceService.Update(model);

                return RedirectToAction("Index","student", new { id = model.Id });
            }

            return View(model);
        }

    }
}
