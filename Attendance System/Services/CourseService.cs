using Attendance_System.Models;

namespace Attendance_System.Services
{
    public class CourseService : ICourseService
    {
        ItiDbContext db;
        public CourseService(ItiDbContext _db)
        {
            db = _db;
        }
        public List<Course> GetAll()
        {
            return db.Courses.ToList();
        }

        public Course GetById(int id)
        {
            return db.Courses.FirstOrDefault(s => s.CrsId == id);
        }
        public void Add(Course course)
        {
            db.Courses.Add(course);
            db.SaveChanges();

        }
        public void Delete(int id)
        {
            var res = db.Courses.FirstOrDefault(s => s.CrsId == id);
            db.Courses.Remove(res);
            db.SaveChanges();

        }
        public void Update(Course crs)
        {
            //var res = db.Students.FirstOrDefault(s => s.Id == id);
            db.Courses.Update(crs);
            db.SaveChanges();

        }
    }
}
