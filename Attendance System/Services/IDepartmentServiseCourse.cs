using Attendance_System.Models;

namespace Attendance_System.Services
{
    public interface IDepartmentServiseCourse
    {

        public Department GetDC(int deptid);
        public Department GetDST(int deptid);

    }
}
