using Attendance_System.Models;
using Attendance_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Attendance_System.Controllers
{
    public class StudentController : Controller
    {
        IStudentService studentService;
        public StudentController(IStudentService _studentService)
        {
            studentService = _studentService;
        }
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var model=studentService.GetById(id.Value);
            return View(model);
        }
        public IActionResult Edit(int? id) 
        {
            if (id == null) 
            {
                return BadRequest();
            }
            var model = studentService.GetById(id.Value);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, string name, string email, string password, int age, string cpassword, IFormFile img)
        {
            var res = studentService.GetById(id);
            if (res == null)
            {
                return NotFound();
            }
            var existingStudent = studentService.Validate(email, id);
            var res2 = studentService.ValidateEmailst(email,id);
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
            return View(res.Id);
        }
        public IActionResult checkemail(int id,string email, string name, int age)
        {
            var res = studentService.GetByemail(email);
            if (res == null)
            {
                return BadRequest();
            }
            if (id == 0)
            {
                var res4 = studentService.ValidateEmail(res);//==
                if (res4 == null) 
                {
                    return Json(true);

                }
                else
                {
                    return Json($"Email Not Vaild you can uses {name}{age}@iti.com");
                }

            }

            var res2 = studentService.ValidateEmailst(email, id);//!=
            var res3 = studentService.ValidateEmail2(res);//==
            if (res2 == null&& res3 != null) 
            {
                return Json(true);
            }
            if (res3 == null) 
            {
                return Json(true);
            }
            if (res2 != null)
                return Json($"Email Not Vaild you can uses {name}{age}@iti.com");

            return Json(true);


        }
    }
}
