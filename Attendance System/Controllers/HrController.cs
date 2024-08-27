using Attendance_System.Models;
using Attendance_System.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Attendance_System.Controllers
{
    public class HrController : Controller
    {
        IHrService hrService;
        IStudentService studentService;
        IUserService userService;
        IUserRoleService userroleService;
        public HrController(IHrService _hrService, IStudentService _studentService, IUserService userService, IUserRoleService userroleService)
        {
            hrService = _hrService;
            studentService = _studentService;
            this.userService = userService;
            this.userroleService = userroleService;
        }
        public IActionResult Details(int? id)
        {
            if (id == null) 
            {
                return BadRequest();
            }
            var model = hrService.GetById(id.Value);

            return View(model);
        }
        public IActionResult EditHr(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            var model = hrService.GetById(id.Value);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditHr(Hr hr, string OldName, string OldEmail, string OldPassword)
        {
            if (hr == null)
            {
                return Content("null");
            }
            var em = hrService.validate(hr.Email,hr.Id);
            if (em == null)
            {
                var res = userroleService.GetBId(hr.Id, 3);
                var res2 = userService.GetByUserNPass(OldName, OldPassword);
                res2.Password = hr.Password;
                res2.UserName = hr.Name;
                userService.Update(res2);
                hrService.Update(hr);
                return RedirectToAction("index", new { hr.Id });
            }
            ModelState.AddModelError("Email", "The email address is already in use.");
            return View(hr);
        }
        public IActionResult Index(int? id) 
        {
            if (id == null)
            {
                return BadRequest();
            }
            ViewBag.Id = id.Value;  
            var model = studentService.GetByHr(id.Value);
            return View(model);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(bool Verified,int id)
        {
            var std = studentService.GetById(id);
            if (std == null) 
            {
                return Content("null");
            }
            std.Verified = Verified;
            studentService.Update(std);
            return RedirectToAction("Index", new {Id=std.Hnum});
        }
        public IActionResult StudentDetails(int? id) 
        {
            if (id == null) 
            {
                return BadRequest();
            }
            var res = studentService.GetById(id.Value);
            if(res == null)
            {
                return NotFound();
            }
            return View(res);
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("account", "i");
        }
        public IActionResult UploadStudents(int? id)
        {
            if (id==null)
            {
                return BadRequest();  
            }
            var model = hrService.GetById(id.Value);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadStudents(int id,IFormFile excelFile)
        {
            if (excelFile == null || excelFile.Length == 0)
            {
                ModelState.AddModelError("", "Please select a file.");
                return View();
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await excelFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.First();
                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++) // Assuming the first row is the header
                        {
                            var student = new Student
                            {
                                Name = worksheet.Cells[row, 1].Text,
                                Email = worksheet.Cells[row, 2].Text,
                                Age = int.TryParse(worksheet.Cells[row, 3].Text, out var age) ? (int?)age : null,
                                Password = worksheet.Cells[row, 4].Text,
                                img = worksheet.Cells[row, 5].Text,
                                Dnum = int.TryParse(worksheet.Cells[row, 6].Text, out var dnum) ? dnum : 0,
                                Hnum = int.TryParse(worksheet.Cells[row, 7].Text, out var hnum) ? hnum : 0,
                                Verified = bool.TryParse(worksheet.Cells[row, 8].Text, out var verified) ? verified : false
                            };

                            // Add student to the database
                            // var x = studentService.GetByUserNPass(student.Name,student.Password);
                            student.CPassword = student.Password;
                            studentService.Add(student);
                            userService.Add(student.Name, student.Password);
                        }
                    }
                }

                return RedirectToAction("Index", new {id=id}); // Redirect to a success page or display a message
            }
            catch (Exception ex)
            {
                // Handle exception and log it
                ModelState.AddModelError("", $"An error occurred while processing the file: {ex.Message}");
                return View();
            }
        }
    }
}
