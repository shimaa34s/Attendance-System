using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Attendance_System.Models
{
    public class Hr
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"[a-zA-Z0-9_]+@[a-zA-z]+.[a-zA-Z]{2,4}")]
        [Remote("checkemail", "hr", AdditionalFields = "Name,Age")]
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Student> Student { get; set; } = new HashSet<Student>();
    }
}
