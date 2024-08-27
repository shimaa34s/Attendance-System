using Attendance_System.Services;
using Microsoft.AspNetCore.Mvc;
using Attendance_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;

namespace Attendance_System.Controllers
{
    public class AdminController : Controller
    {
        IHrService hrservice;
        IUserService userservice;
        IUserRoleService userRoleService;
        IStudentService studentService;
        IStudentAttendeceService studentAttendeceService;
        IDepartmentService departmentService;
        IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<PdfGenerator> _logger;
        public AdminController(ILogger<PdfGenerator> _logger, IWebHostEnvironment webHostEnvironment, IHrService _hrservice, IUserService _userService, IUserRoleService _userRoleService, IStudentService studentService, IStudentAttendeceService studentAttendeceService, IDepartmentService departmentService)
        {
            hrservice = _hrservice;
            userservice = _userService;
            userRoleService = _userRoleService;
            this.studentService = studentService;
            this.studentAttendeceService = studentAttendeceService;
            this.departmentService = departmentService;
            this.webHostEnvironment = webHostEnvironment;
            this._logger = _logger;
        }
        public IActionResult StList()
        {
            var model = studentService.GetAll();
            return View(model);
        }
        public IActionResult Index()
        {
            var model = hrservice.GetAll();
            return View(model);
        }
        public ActionResult CreateHr()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateHr(Hr hr)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                string randomInt = random.Next().ToString();
                hr.Password = randomInt;
                hrservice.Add(hr);
                userservice.Add(hr.Name, randomInt);
                var res = userservice.GetByUserNPass(hr.Name, hr.Password);
                userRoleService.Add(new() { RId = 3, UId = res.Id });
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult details(int id)
        {
            var model = hrservice.GetById(id);
            return View(model);
        }
        public IActionResult edit(int id)
        {
            var model = hrservice.GetById(id);
            return View(model);

        }
        [HttpPost]
        public IActionResult edit(Hr hr)
        {
            var emailinhr = hrservice.Getbyemail(hr);
            if (emailinhr != null)
            {
                ModelState.AddModelError("Email", "The email address is already in us");
            }
            if (ModelState.IsValid)
            {
                hrservice.Update(hr);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult delete(int id)
        {
            var hr = hrservice.GetById(id);
            var res = userservice.GetByUserNPass(hr.Name, hr.Password);
            hrservice.Delete(hr);
            userservice.Delete(res);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteStudent(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model = studentService.GetById(id.Value);
            return View(model);
        }
        public IActionResult EditStudent(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var model = studentService.GetById(id.Value);
            List<Hr> hr = hrservice.GetAll();
            SelectList hrl = new SelectList(hr, "Id", "Name");
            List<Department> dept = departmentService.GetAll();
            SelectList deptl = new SelectList(dept, "DeptId", "DeptName");
            ViewBag.deptl = deptl;
            ViewBag.hrl = hrl;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditStudent(int id, string name, string email, string password, int age, string cpassword, IFormFile img)
        {
            var res = studentService.GetById(id);
            if (res == null)
            {
                return NotFound();
            }
            var existingStudent = studentService.Validate(email, id);
            var res2 = studentService.ValidateEmailst(email, id);
            if (existingStudent != null)
            {
                ModelState.AddModelError("Email", "The email address is already in use.");
            }
            if (res2 != null)
            {
                ModelState.AddModelError("Email", "The email address is already in use.");
            }
            if (!ModelState.IsValid)
            {
                return View(res);
            }
            res.Name = name;
            res.Email = email;
            res.Password = password;
            res.Age = age;
            res.CPassword = cpassword;
            if (img != null)
            {
                res.img = $"{res.Id}" + '.' + img.FileName.Split('.').Last();
                using (FileStream st = new FileStream($"wwwroot/images/{res.img}", FileMode.Create))
                {
                    await img.CopyToAsync(st);
                }
            }
            if (ModelState.IsValid)
            {
                studentService.Update(res);
                return RedirectToAction("Index", new { id = res.Id });
            }
            List<Hr> hr = hrservice.GetAll();
            SelectList hrl = new SelectList(hr, "Id", "Name");
            List<Department> dept = departmentService.GetAll();
            SelectList deptl = new SelectList(dept, "DeptId", "DeptName");
            ViewBag.deptl = deptl;
            ViewBag.hrl = hrl;
            return View(res.Id);
        }
        // [HttpGet]
        public IActionResult CreateST()
        {
            List<Hr> hr = hrservice.GetAll();
            SelectList hrl = new SelectList(hr, "Id", "Name");
            List<Department> dept = departmentService.GetAll();
            SelectList deptl = new SelectList(dept, "DeptId", "DeptName");
            ViewBag.deptl = deptl;
            ViewBag.hrl = hrl;
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> CreateST(Student std, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                if (std.img != null)
                {
                    std.img = $"{std.Id}" + '.' + img.FileName.Split('.').Last();
                    using (FileStream st = new FileStream($"wwwroot/images/{std.img}", FileMode.Create))
                    {
                        await img.CopyToAsync(st);
                    }
                }
                var res = studentService.ValidateEmail(std);
                if (res == null)
                {
                    studentService.Add(std);
                    return RedirectToAction("StList");
                }
                else
                {
                    ModelState.AddModelError("Email", "The email address is already in use.");
                    List<Hr> hr = hrservice.GetAll();
                    SelectList hrl = new SelectList(hr, "Id", "Name");
                    List<Department> dept = departmentService.GetAll();
                    SelectList deptl = new SelectList(dept, "DeptId", "DeptName");
                    ViewBag.deptl = deptl;
                    ViewBag.hrl = hrl;
                    return View();
                }

            }

            return View();
        }
        public IActionResult ShowAttendance()
        {
            var model = studentService.GetAllStData();

            return View(model);
        }
        public IActionResult GenerateStudentPdf(int id)
        {
            try
            {
                var pdfGenerator = new PdfGenerator(studentService, webHostEnvironment, _logger);
                pdfGenerator.GenerateStudentCardPdf(id);

                var filePath = Path.Combine(webHostEnvironment.WebRootPath, $"Student_{id}_Card.pdf");

                if (System.IO.File.Exists(filePath))
                {
                    return File(System.IO.File.ReadAllBytes(filePath), "application/pdf", $"Student_{id}_Card.pdf");
                }
                else
                {
                    _logger.LogWarning("The PDF file could not be found.");
                    return NotFound("The PDF file could not be found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating the PDF.");
                return BadRequest("An error occurred while generating the PDF. Please try again later.");
            }
        }
    }
}
