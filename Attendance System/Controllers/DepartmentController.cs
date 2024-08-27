using Attendance_System.Models;
using Attendance_System.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Attendance_System.ViewModel;
namespace Attendance_System.Controllers
{
    public class DepartmentController : Controller
    {
        
            IDepartmentService deptservice;
            ICourseService courseservice;
            IDepartmentServiseCourse deptcourse;
            IStudentCourseService studentcourse;
            IStudentService studentservice;
            public DepartmentController(IStudentService studentService,IDepartmentService _departmentService, ICourseService _courseservice, IDepartmentServiseCourse _deptcourse, IStudentCourseService studentcourse)
            {
                deptservice = _departmentService;
                courseservice = _courseservice;
                deptcourse = _deptcourse;
                this.studentcourse = studentcourse;
                studentservice = studentService;
            }
            public IActionResult Index()
            {
                List<Department> model = deptservice.GetAll();
                return View(model);
            }
            public IActionResult Details(int? id)
            {
                if (id == null)
                {
                    return BadRequest();
                }
                Department model = deptservice.GetById(id.Value);
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
            public IActionResult create(Department dept)
            {
                if (ModelState.IsValid)
                {
                    deptservice.Add(dept);
                    return RedirectToAction("index");
                }
                return View();
            }
            public IActionResult update()
            {
                List<Department> model = deptservice.GetAll();
                return View(model);
            }
            [HttpPost]
            public IActionResult update(Department dept1)
            {
                deptservice.Update(dept1);
                return RedirectToAction("index");
                //return Content("added");
            }
            public IActionResult delete()
            {
                List<Department> model = deptservice.GetAll();
                return View(model);
            }
            [HttpPost]
            public IActionResult delete(int DeptId)
            {
                //Department dept = deptservice.GetById(DeptId);
                deptservice.Delete(DeptId);
                //db.Remove(dept);
                // db.SaveChanges();
                return RedirectToAction("index");
            }
            public IActionResult edit(int? id)
            {
                if (id == null)
                {
                    return BadRequest();
                }
                Department model = deptservice.GetById(id.Value);
                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }
            [HttpPost]
            public IActionResult edit(Department dept, int id)
            {
                 dept.status = true;
                deptservice.Update(dept);
                return RedirectToAction("Index");
            }
            public IActionResult deletee(int? id)
            {
                Department model = deptservice.GetById(id.Value);
                return View(model);

            }
            [ActionName("deletee"), HttpPost]
            public IActionResult deleteeC(int? id)
            {
                // Department model = deptservice.GetById(id.Value);
                deptservice.Delete(id.Value);
                return RedirectToAction("index");

            }

            public IActionResult checkid(int DeptId)
            {
                var res = deptservice.GetById(DeptId);
                if (res != null)
                {
                    return Json("Dublicated id");
                }
                return Json(true);
            }
            public IActionResult ShowCourse(int? id)
            {
                if (id == null)
                {
                    return BadRequest();
                }
                var res = deptcourse.GetDC(id.Value);
                if (res == null)
                {
                    return NotFound();
                }
                return View(res);
            }
            public IActionResult MangeCourses(int? id)
            {
                if (id == null)
                {
                    return BadRequest();
                }
                var allcourse = courseservice.GetAll();
                var coursein = deptcourse.GetDC(id.Value);
                var courseout = allcourse.Except(coursein.Course);
                ViewBag.coursein = coursein.Course;
                ViewBag.courseout = courseout;
                var dept = deptservice.GetById(id.Value);
                return View(dept);
            }
            [HttpPost]
            public IActionResult MangeCourses(int id, List<int> coursetoremove, List<int> coursetoadd)
            {
                var dept = deptcourse.GetDC(id);
                foreach (var c in coursetoremove)
                {
                    var crs = courseservice.GetById(c);
                    dept.Course.Remove(crs);
                    // dept.Course.Remove(crs);
                }
                foreach (var c in coursetoadd)
                {
                    var crs = courseservice.GetById(c);
                    dept.Course.Add(crs);

                    //dept.Course.Add(crs);
                    // courseservice.Add(crs);
                }
            // db.SaveChanges();
            deptservice.Save(dept);
                return RedirectToAction("ShowCourse", "department", new { id = id });
            }

            public IActionResult adddegree(int deptid, int crsid)
            {
                Degree model = new Degree();
                var dept = deptcourse.GetDST(deptid);
                model.Department = dept;
                var crs = courseservice.GetById(crsid);
                model.Course= crs;
                var stdegre = studentcourse.GetAll();

                model.StudentCourse = stdegre;

                // ViewBag.dept = dept;

                return View(model);
            }
            [HttpPost]
            public IActionResult adddegree(int deptid, int crsid, Dictionary<int, int> deg)
            {
                foreach (var item in deg)
                {
                    var res = studentcourse.GetById(item.Key, crsid);
                    //   db.StudentCourse.FirstOrDefault(s=>s.crsid == crsid && s.stId == item.Key);
                    if (res == null)
                    {
                        studentcourse.Add(new StudentCourse { stId = item.Key, crsid = crsid, degree = item.Value });
                    }
                    else
                    {
                        res.degree = item.Value;
                    }
                     studentcourse.Update(res);
                }

                //db.SaveChanges(); 
                return RedirectToAction("Index");
            }
        public IActionResult adddintake(int deptid, int crsid)
        {
            Degree model = new Degree();
            var dept = deptcourse.GetDST(deptid);
            model.Department = dept;
            var crs = courseservice.GetById(crsid);
            model.Course = crs;
            var stdegre = studentcourse.GetAll();

            model.StudentCourse = stdegre;

            // ViewBag.dept = dept;

            return View(model);
        }
        [HttpPost]
        public IActionResult adddintake(int deptid, int crsid, Dictionary<int, string> deg)
        {
            List<string> errorMessages = new List<string>();
            foreach (var item in deg)
            {
                var stId = item.Key;
                var intakeValue = item.Value;
                DateTime? intakeDate = null;
                var res2=studentservice.GetById(stId);
                if (!string.IsNullOrWhiteSpace(intakeValue))
                {
                    if (DateTime.TryParse(intakeValue, out DateTime parsedDate))
                    {
                        if (parsedDate < new DateTime(2001, 1, 1) || parsedDate > new DateTime(2099, 12, 31))
                        {
                            errorMessages.Add($"{res2.Name} The date must be between 01/01/2001, 12/31/2099");
                        }
                        else
                        {
                            intakeDate = parsedDate;
                        }
                    }
                    else
                    {
                        errorMessages.Add($" {res2.Name} The date must be between 01/01/2001, 12/31/2099.");
                    }
                }

                var res = studentcourse.GetById(stId, crsid);
                if (res == null)
                {
                    if (intakeDate.HasValue)
                    {
                        studentcourse.Add(new StudentCourse
                        {
                            stId = stId,
                            crsid = crsid,
                            degree = 0, 
                            intake = intakeDate
                        });
                    }
                }
                else
                {
                    res.degree = 0; 
                    res.intake = intakeDate;
                    studentcourse.Update(res);
                }
            }

            if (errorMessages.Count > 0)
            {
                ViewBag.ErrorMessages = errorMessages;
                return View(new Degree
                {
                    Department = deptcourse.GetDST(deptid),
                    Course = courseservice.GetById(crsid),
                    StudentCourse = studentcourse.GetAll()
                });
            }

            return RedirectToAction("Index");
        }




        //[HttpPost]
        //public IActionResult adddintake(int deptid, int crsid, Dictionary<int, int> deg)
        //{
        //    if (ModelState.IsValid) 
        //    {
        //        foreach (var item in deg)
        //        {
        //            var res = studentcourse.GetById(item.Key, crsid);
        //            if (res == null)
        //            {
        //                studentcourse.Add(new StudentCourse { stId = item.Key, crsid = crsid, degree = item.Value });
        //            }
        //            else
        //            {
        //                res.degree = item.Value;
        //            }
        //            studentcourse.Update(res);
        //        }
        //        return RedirectToAction("Index");

        //    }
        //    ModelState.AddModelError("intake", "The date must be greater than 01/01/2001");
        //    return View();
        //  //  return View( new { id = deptid }, new {id=crsid});

    }
}

