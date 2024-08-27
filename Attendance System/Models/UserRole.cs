using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Attendance_System.Models
{
    public class UserRole
    {
        [ForeignKey("user")]
        public int UId { get; set; }
        [ForeignKey("Role")]

        public int RId { get; set; }
        public virtual User user { get; set; }

        public virtual Role Role { get; set; }
    }
}
