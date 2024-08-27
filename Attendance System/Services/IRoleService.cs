using Attendance_System.Models;

namespace Attendance_System.Services
{
    public interface IRoleService
    {
        public Role GetRU(int id);
        public int GetBiD(User user);

    }
}
