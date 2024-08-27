using Attendance_System.Models;

namespace Attendance_System.Services
{
    public interface IUserRoleService
    {
        public UserRole GetBId(int uid, int rid);
        public void Add(UserRole userRole);
    }
}
