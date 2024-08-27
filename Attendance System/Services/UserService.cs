using Attendance_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance_System.Services
{
    public class UserService:IUserService
    {
        ItiDbContext db;
        public UserService(ItiDbContext _db)
        {
            db = _db;
        }
        public void Update(User user)
        {
            db.Users.Update(user);
            db.SaveChanges();
        }
        public void Delete(User user)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public User GetById(int id)
        {
            return db.Users.SingleOrDefault(u => u.Id == id);
        }

        public void Add(string username, string password)
        {
            User user = new User() { UserName = username, Password = password };
            db.Users.Add(user);
            db.SaveChanges();
           

        }
        public User GetByUserNPass(string username, string password)
        {
            return db.Users.Include(s => s.UserRole).FirstOrDefault(s => s.UserName == username && s.Password == password);
        }

    }
}
