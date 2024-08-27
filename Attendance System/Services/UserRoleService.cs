using Attendance_System.Models;

namespace Attendance_System.Services
{
    public class UserRoleService: IUserRoleService
    {
        ItiDbContext db;
        public UserRoleService(ItiDbContext _db)
        {
            db = _db;
        }
        public UserRole GetBId(int uid, int rid)
        {
            return db.UserRoles.FirstOrDefault(s => s.UId == uid && s.RId == rid);
        }
        public void Add(UserRole userRole)
        {
            db.UserRoles.Add(userRole);
            db.SaveChanges();
        }

    }
}
