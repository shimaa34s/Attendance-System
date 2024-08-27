using OfficeOpenXml;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Attendance_System.Models;
using Attendance_System.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Attendance_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // تعيين سياق الترخيص لـ EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var builder = WebApplication.CreateBuilder(args);

            // قراءة سلسلة الاتصال من ملف الإعدادات
            string cs = builder.Configuration.GetConnectionString("con1");

            // إضافة الخدمات إلى الحاوية
            builder.Services.AddControllersWithViews();

            // تكوين DbContext لاستخدام قاعدة بيانات SQL Server
            builder.Services.AddDbContext<ItiDbContext>(options =>
                options.UseSqlServer(cs));

            // إضافة خدمات أخرى (Scoped)
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IStudentAttendeceService, StudentAttendeceService>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IStudentCourseService, StudentCourseService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRoleService, UserRoleService>();
            builder.Services.AddScoped<IHrService, HrService>();

            // إعداد المصادقة باستخدام ملفات تعريف الارتباط
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/account/login"; // تحديد مسار تسجيل الدخول
                });

            // بناء التطبيق
            var app = builder.Build();

            // تكوين خط أنابيب الطلبات HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseDeveloperExceptionPage(); // صفحة استثناءات التطويرية في وضع التطوير
            }

            app.UseStaticFiles(); // استخدام ملفات ثابتة (مثل CSS، JS، إلخ)

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // تكوين مسار التحكم الافتراضي
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=admin}/{action=index}/{id?}");

            app.Run();
        }
    }
}
