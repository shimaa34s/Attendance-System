﻿using Attendance_System.Models;
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
                    int roleId = roleService.GetBiD(TU);
                    var role = roleService.GetRU(roleId);

                    // Create claims
                    Claim c1 = new Claim(ClaimTypes.Email, $"{model.UserName}@iti.gov");
                    Claim c2 = new Claim(ClaimTypes.Name, model.Password);
                    Claim c3 = new Claim(ClaimTypes.Role, role.Name);

                    ClaimsIdentity ci = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    ci.AddClaim(c1);
                    ci.AddClaim(c2);
                    ci.AddClaim(c3);

                    // Add HRId claim if the user is an HR
                    if (roleId == 3)
                    {
                        var hrUser = hrService.GetbyemailAndName(TU);
                        Claim hrIdClaim = new Claim("HRId", hrUser.Id.ToString());
                        ci.AddClaim(hrIdClaim);
                    }

                    ClaimsPrincipal cip = new ClaimsPrincipal(ci);
                    await HttpContext.SignInAsync(cip);

                    // Redirect based on role
                    if (roleId == 1) // Admin
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (roleId == 3) // HR
                    {
                        var hrUser = hrService.GetbyemailAndName(TU);
                        return RedirectToAction("Details", "Hr", new { id = hrUser.Id });
                    }
                    else if (roleId == 2) // Student
                    {
                        var student = studentService.GetByUserNPass(model.UserName, model.Password);

                        if (student != null)
                        {
                            return RedirectToAction("Index", "Student", new { id = student.Id });
                        }

                        studentService.Add(new Student()
                        {
                            Name = TU.UserName,
                            Password = TU.Password,
                            Dnum = 100,
                            Hnum = 10
                        });

                        student = studentService.GetByUserNPass(model.UserName, model.Password);
                        return RedirectToAction("Index", "Student", new { id = student.Id });
                    }

                    // Add user-role association if not already present
                    var userWithRole = userRoleService.GetBId(role.Id, roleId);
                    if (roleId != role.Id && userWithRole == null)
                    {
                        userRoleService.Add(new UserRole() { UId = TU.Id, RId = role.Id });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View(model);
                }
            }

            return View(model);
        }

        //public async Task<IActionResult> Login(LoginVM model)
        //{
        //    var TU = userService.GetByUserNPass(model.UserName, model.Password);

        //    if (ModelState.IsValid)
        //    {
        //        if (TU != null)
        //        {
        //            int roleId = roleService.GetBiD(TU);
        //            var role = roleService.GetRU(roleId);

        //            Claim c1 = new Claim(ClaimTypes.Email, $"{model.UserName}@iti.gov");
        //            Claim c2 = new Claim(ClaimTypes.Name, model.Password);
        //            Claim c3 = new Claim(ClaimTypes.Role, role.Name);

        //            ClaimsIdentity ci = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        //            ci.AddClaim(c1);
        //            ci.AddClaim(c2);
        //            ci.AddClaim(c3);
        //            ClaimsPrincipal cip = new ClaimsPrincipal(ci);

        //            await HttpContext.SignInAsync(cip);

        //            if (roleId == 1) 
        //            {
        //                return RedirectToAction("Index", "Admin");
        //            }
        //            else if (roleId == 3) 
        //            {
        //                var res = userService.GetByUserNPass(TU.UserName, TU.Password);
        //                var res2 = hrService.GetbyemailAndName(res);
        //                return RedirectToAction("Details", "Hr", new { id = res2.Id });
        //            }
        //            else if (roleId == 2) 
        //            {
        //                var student = studentService.GetByUserNPass(model.UserName, model.Password);

        //                if (student != null)
        //                {
        //                    return RedirectToAction("Index", "Student", new { id = student.Id });
        //                }

        //                studentService.Add(new Student()
        //                {
        //                    Name = TU.UserName,
        //                    Password = TU.Password,
        //                    Dnum = 100,
        //                    Hnum = 10
        //                });

        //                student = studentService.GetByUserNPass(model.UserName, model.Password);
        //                return RedirectToAction("Index", "Student", new { id = student.Id });
        //            }

        //            var userWithRole = userRoleService.GetBId(role.Id, roleId);
        //            if (roleId != role.Id && userWithRole == null)
        //            {
        //                userRoleService.Add(new UserRole() { UId = TU.Id, RId = role.Id });
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Incorrect username or password");
        //            return View(model);
        //        }
        //    }

        //    return View(model);
        //}

        //public async Task<IActionResult> Login(LoginVM model)
        //{
        //    var TU = userService.GetByUserNPass(model.UserName, model.Password);
        //    if (ModelState.IsValid)
        //    {
        //        if (TU != null)
        //        {
        //            int id = roleService.GetBiD(TU);
        //            var Role = roleService.GetRU(2);

        //            if (id == 1) // إذا كان المستخدم admin
        //            {
        //                return RedirectToAction("Index", "Admin");
        //            }
        //            else if (id == 3) // إذا كان المستخدم hr
        //            {
        //                var res = userService.GetByUserNPass(TU.UserName, TU.Password);
        //                var res2 = hrService.GetbyemailAndName(res);
        //                return RedirectToAction("Details", "Hr", new { id = res2.Id });
        //            }

        //            // إعداد المطالبات الخاصة بالمستخدم
        //            Claim c1 = new Claim(ClaimTypes.Email, $"{model.UserName}@iti.gov");
        //            Claim c2 = new Claim(ClaimTypes.Name, model.Password);
        //            Claim c3;

        //            // إذا كان المستخدم ليس له دور بعد، يتم تعيينه للدور المناسب
        //            if (id == 0)
        //            {
        //                userRoleService.Add(new UserRole() { UId = TU.Id, RId = Role.Id });
        //            }
        //            var userWithRole = userRoleService.GetBId(Role.Id, id);
        //            if (id != Role.Id && userWithRole == null)
        //            {
        //                userRoleService.Add(new UserRole() { UId = TU.Id, RId = Role.Id });
        //            }
        //            c3 = new Claim(ClaimTypes.Role, Role.Name);

        //            ClaimsIdentity ci = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        //            ci.AddClaim(c1);
        //            ci.AddClaim(c2);
        //            ci.AddClaim(c3);
        //            ClaimsPrincipal cip = new ClaimsPrincipal(ci);
        //            await HttpContext.SignInAsync(cip);

        //            var res5 = studentService.GetByUserNPass(model.UserName, model.Password);

        //            if (res5 != null)
        //            {
        //                return RedirectToAction("Index", "Student", new { id = res5.Id });
        //            }

        //            studentService.Add(new() { Name = TU.UserName, Password = TU.Password, Dnum = 100, Hnum = 10 });
        //            return RedirectToAction("Index", "Student", new { id = res5.Id });
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Incorrect username or password");
        //            return View(model);
        //        }
        //    }
        //    return View();
        //}


        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("login");
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
