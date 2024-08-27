using Attendance_System.Models;

namespace Attendance_System.Services
{
    public class StudentCourseService:IStudentCourseService
    {
        ItiDbContext db;
        public StudentCourseService(ItiDbContext _db)
        {
            db = _db;
        }
        public List<StudentCourse> GetAll()
        {
            return db.StudentCourse.ToList();
        }
        public StudentCourse GetById(int stid, int crsid)
        {
            return db.StudentCourse.FirstOrDefault(s => s.stId == stid && s.crsid == crsid);

        }
        //public void Save(StudentCourse stc)
        //{
        //    db.Update(stc);
        //    db.SaveChanges();
        //}
        public void Delete(int stid, int crsid)
        {
            var res = db.StudentCourse.FirstOrDefault(s => s.stId == stid && s.crsid == crsid);
            db.StudentCourse.Remove(res);
            db.SaveChanges();
        }
        public void Update(StudentCourse stdc)
        {
            db.StudentCourse.Update(stdc);
            db.SaveChanges();
        }
        public void Add(StudentCourse studentcourse)
        {
            db.StudentCourse.Add(studentcourse);
            db.SaveChanges();
        }
    }
}
