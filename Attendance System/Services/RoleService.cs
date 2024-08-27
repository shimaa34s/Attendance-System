using Attendance_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance_System.Services
{
    public class RoleService:IRoleService
    {
        ItiDbContext db;
        public RoleService(ItiDbContext _db)
        {
            db = _db;
        }
        public Role GetRU(int id)
        {
            return db.Roles.Include(s => s.UserRole).SingleOrDefault(s => s.Id == id);
        }
        public int GetBiD(User user)
        {
            if (user != null)
            {
                foreach (var item in user.UserRole)
                {
                    return item.RId;
                }
            }
            else
            {
                return 0;
            }

            return 0;
        }
    }
}
