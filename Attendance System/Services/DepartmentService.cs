using Attendance_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance_System.Services
{
    public class DepartmentService:IDepartmentService,IDepartmentServiseCourse
    {
        
        ItiDbContext db;
        public DepartmentService(ItiDbContext _db)
        {
            db = _db;
        }
        public List<Department> GetAll()
        {
            return db.Departments.Where(s => s.status == true).ToList();
        }
     

        public Department GetById(int id)
        {
            return db.Departments.FirstOrDefault(s => s.DeptId == id);
        }
        public void Add(Department department)
        {
            department.status = true;
            db.Departments.Add(department);
            db.SaveChanges();

        }
        public void Delete(int id)
        {
            var res = db.Departments.FirstOrDefault(s => s.DeptId == id);
            //db.Departments.Remove(res);
            res.status = false;

            db.SaveChanges();

        }
        public void Save(Department department)
        {
            department.status = true;
            db.SaveChanges();
        }
        public void Update(Department dept)
        {
            db.Departments.Update(dept);
            db.SaveChanges();
        }
        public Department GetDC(int deptid)
        {
            return db.Departments.Include(s => s.Course).FirstOrDefault(s => s.DeptId == deptid);
        }
        public Department GetDST(int deptid)
        {
            return db.Departments.Include(s => s.student).FirstOrDefault(s => s.DeptId == deptid);

        }

    }
}
