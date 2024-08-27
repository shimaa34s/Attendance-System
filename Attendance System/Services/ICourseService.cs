using Attendance_System.Models;

namespace Attendance_System.Services
{
    public interface ICourseService
    {
        public List<Course> GetAll();
        public Course GetById(int id);

        public void Delete(int id);
        public void Update(Course std);
        public void Add(Course student);
    }
}
