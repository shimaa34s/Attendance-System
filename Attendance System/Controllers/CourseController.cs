using Attendance_System.Models;
using Attendance_System.Services;
using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;

namespace Attendance_System.Controllers
{
    public class CourseController : Controller
    {
        // ItiDbContext db=new ItiDbContext();
        ICourseService courseService;
        public CourseController(ICourseService _courseService)
        {
            courseService = _courseService;
        }
        public IActionResult Index()
        {
            List<Course> model = courseService.GetAll();
            return View(model);
        }
        public IActionResult details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Course model = courseService.GetById(id.Value);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult create(Course crs)
        {
            if (ModelState.IsValid)
            {
                courseService.Add(crs);
                return RedirectToAction("index");
            }
            return View();

        }
        public ActionResult update()
        {
            List<Course> model = courseService.GetAll();
            return View(model);
        }
        [HttpPost]
        public ActionResult update(Course crs)
        {
            //Course course = courseService.GetById(crs.CrsId);
            //course.CrsName=crs.CrsName;
            //course.Duration=crs.Duration;
            //db.SaveChanges();
            courseService.Update(crs);
            return RedirectToAction("index");
        }
        public ActionResult delete()
        {
            List<Course> model = courseService.GetAll();
            return View(model);
        }
        [HttpPost]
        public ActionResult delete(Course crs)
        {
            courseService.Delete(crs.CrsId);
            return RedirectToAction("index");
        }
        public IActionResult edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Course model = courseService.GetById(id.Value);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult edit(Course crs, int id)
        {
            var res = courseService.GetById(id);
            if (res == null)
            {
                return BadRequest();
            }
            res.CrsName = crs.CrsName;
            res.CrsId = id;
            res.Duration = crs.Duration;
            res.StudentCourse = crs.StudentCourse;
            res.Department= crs.Department;
            courseService.Update(res);
            return RedirectToAction("index");
        }
        public IActionResult deletee(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Course crs = courseService.GetById(id.Value);

            return View(crs);
        }
        [ActionName("deletee"), HttpPost]
        public IActionResult deleteeC([FromForm] int? CrsId)
        {
            //Course crs = courseService.GetById(id.Value);
            courseService.Delete(CrsId.Value);
            return RedirectToAction("index");
        }

    }
  
}
