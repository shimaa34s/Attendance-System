using Attendance_System.Models;

namespace Attendance_System.ViewModel
{
    public class Degree
    {
        public Department Department { get; set; }
        public Course Course { get; set; }
        public List<StudentCourse> StudentCourse { get; set; }
    }
}
