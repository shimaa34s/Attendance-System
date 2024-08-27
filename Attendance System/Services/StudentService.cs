using Attendance_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance_System.Services
{
    public class StudentService:IStudentService
    {
        ItiDbContext db;
        public StudentService(ItiDbContext _db)
        {
            db = _db;
        }
        public List<Student> GetAll()
        {
            return db.Students.Include(s=>s.Department).ToList();
        }
        public Student GetById(int id)
        {
            return db.Students.FirstOrDefault(s => s.Id == id);
        }
        public Student GetByemail(string email)
        {
            return db.Students.FirstOrDefault(s => s.Email == email);

        }
        public void Delete(int id)
        {
            var res=db.Students.FirstOrDefault(s=>s.Id==id);
            db.Students.Remove(res);
            db.SaveChanges();
        }
        public void Update(Student std)
        {
            db.Students.Update(std);
            db.SaveChanges();
        }
        public void Add(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();
        }

        public Student ValidateEmail(Student student)
        {
            return db.Students.Where(s => s.Email == student.Email && s.Id != student.Id).FirstOrDefault();

        }
        public Student ValidateEmailst(string email,int id)
        {
            return db.Students.Where(s => s.Email == email && s.Id != id).FirstOrDefault();

        }
        public Student ValidateEmail2(Student student)
        {
            return db.Students.Where(s => s.Email == student.Email && s.Id == student.Id).FirstOrDefault();

        }
        public Student ValidateName(Student student)
        {
            return db.Students.Where(s => s.Name == student.Name && s.Id != student.Id).FirstOrDefault();

        }
        public Student Validate(string email, int id)
        {
            return db.Students.Where(s => s.Name == email && s.Id !=id).FirstOrDefault();

        }
        public Student ValidateEmail(string email)
        {
            return db.Students.FirstOrDefault(s=>s.Email==email);
        }

        public Student GetByUserNPass(string name, string password)
        {
            return db.Students.FirstOrDefault(s => s.Name == name && s.Password == password);
        }
        public List<Student> GetByHr(int hnum)
        {
            return db.Students.Where(s=>s.Hnum== hnum).ToList();
        }
        public List<Student> GetHrDept()
        {
            return db.Students
                      .Include(s => s.Department)
                      .Include(s => s.Hr)          
                      .ToList();
        }
        public List<Student> GetAllStData()
        {
            return db.Students.Include(s => s.StudentAttendece).Where(a => a.Verified == true).ToList();
        }
    }
}
