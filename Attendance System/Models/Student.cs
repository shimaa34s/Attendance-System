using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Attendance_System.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }
        [RegularExpression(@"[a-zA-Z0-9_]+@[a-zA-z]+.[a-zA-Z]{2,4}")]
        [Remote("checkemail", "Student", AdditionalFields = "Id,Name,Age")]
        public string Email { get; set; }
        [Range(10, 100)]
        public int? Age { get; set; }
        [Required]
        public string Password { get; set; }
        [Compare("Password")]
        [NotMapped]
        public string CPassword { get; set; }
        public string? img { get; set; }

        [ForeignKey("Department")]
        public int Dnum { get; set; }
        [ForeignKey("Hr")]
        public int Hnum { get; set; }
 

        public bool Verified { get; set; }



        public virtual Department Department { get; set; }
        public virtual StudentAttendece StudentAttendece { get; set; }
        public virtual Hr Hr { get; set; }
        public virtual ICollection<StudentCourse> StudentProgram { get; set; } = new HashSet<StudentCourse>();

    }
}
