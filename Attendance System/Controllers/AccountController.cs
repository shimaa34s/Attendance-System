using Attendance_System.Models;
using Attendance_System.Services;
using Attendance_System.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Attendance_System.Controllers
{
    public class AccountController : Controller
    {
        IStudentService studentService;
        IUserService userService;
        IUserRoleService userRoleService;
        IRoleService roleService;
        IHrService hrService;
        public AccountController(IStudentService _studentService, IUserService _userService,IUserRoleService _userroleService,IRoleService _roleService,IHrService _hrService)
        {
            studentService=_studentService;
            userService = _userService;
            userRoleService = _userroleService;
            roleService = _roleService;
            hrService = _hrService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {

            var TU = userService.GetByUserNPass(model.UserName, model.Password);
            if (ModelState.IsValid)
            {
                if (TU != null)
                {
                    int id = roleService.GetBiD(TU);
                    var Role = roleService.GetRU(2);
                    if (id == 1)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    if (id == 3) 
                    {
                        var res=userService.GetByUserNPass(TU.UserName,TU.Password);
                        var res2 = hrService.GetbyemailAndName(res);
                        return RedirectToAction("Details", "Hr", new { id = res2.Id });
                    }
                    Claim c1 = new Claim(ClaimTypes.Email, $"{model.UserName}@iti.gov");
                    Claim c2 = new Claim(ClaimTypes.Name, model.Password);
                    Claim c3;
                    if (id == 0)
                    {
                        userRoleService.Add(new UserRole() { UId = TU.Id, RId = Role.Id });
                    }
                    var userwithrole = userRoleService.GetBId(Role.Id, id);
                    if (id != Role.Id && userwithrole == null)
                    {
                        userRoleService.Add(new UserRole() { UId = TU.Id, RId = Role.Id });
                    }
                    c3 = new Claim(ClaimTypes.Role, Role.Name);
                    ClaimsIdentity ci = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    ci.AddClaim(c1);
                    ci.AddClaim(c2);
                    ci.AddClaim(c3);
                    ClaimsPrincipal cip = new ClaimsPrincipal(ci);
                    await HttpContext.SignInAsync(cip);
                    // var res4 = studentService.GetByUserNPass(model.UserName,model.Password);
                    var res5 = studentService.GetByUserNPass(model.UserName, model.Password);

                    if (res5!=null)
                    {
                        return RedirectToAction("Index", "Student", new { id = res5.Id });
                    }
                    studentService.Add(new() {Name=TU.UserName,Password=TU.Password,Dnum=100,Hnum=10});
                    // res5=studentService.GetByUserNPass(model.UserName,model.Password);
                    return RedirectToAction("Index", "Student", new {id=res5.Id });
                }
                else
                {
                    ModelState.AddModelError("", "incorrect username or password");
                    return View(model);
                }
            }
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "student");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            var res2 = userService.GetByUserNPass(user.UserName, user.Password);
            var res = studentService.GetByUserNPass(user.UserName,user.Password);
            if (res2 == null && res == null)
            {
                userService.Add(user.UserName, user.Password);
            }
            else
            {
                ModelState.AddModelError("", "this username is already used");
                return View();
            }
            return RedirectToAction("Login");
        }
        public IActionResult accessDenied()
        {
            return View();
        }
    }
}
