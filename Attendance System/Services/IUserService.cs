using Attendance_System.Models;

namespace Attendance_System.Services
{
    public interface IUserService
    {
        public void Add(string username, string password);
        public User GetByUserNPass(string username, string password);
        public User GetById(int id);
        public void Update(User user);
        public void Delete(User user);

    }
}
