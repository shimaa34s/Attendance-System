using Attendance_System.Models;

namespace Attendance_System.Services
{
    public interface IStudentAttendeceService
    {
        public StudentAttendece GetById(int id);
        public void Add(StudentAttendece st);
        public void Update(StudentAttendece st);


    }
}
