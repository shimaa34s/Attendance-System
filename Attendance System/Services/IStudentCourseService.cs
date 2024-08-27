using Attendance_System.Models;

namespace Attendance_System.Services
{
    public interface IStudentCourseService
    {
        public List<StudentCourse> GetAll();
        public StudentCourse GetById(int stid, int crsid);

        public void Delete(int stid, int crsid);
        public void Update(StudentCourse std);
        public void Add(StudentCourse student);
    }
}
