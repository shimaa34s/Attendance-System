using System.ComponentModel.DataAnnotations;

namespace Attendance_System.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string UserName { get; set; }
        [DataType(DataType.Password), Required]
        public string Password { get; set; }

        public virtual List<UserRole> UserRole { get; set; } = new List<UserRole>();
    }
}
