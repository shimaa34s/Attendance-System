using System.ComponentModel.DataAnnotations;

namespace Attendance_System.ViewModel
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
