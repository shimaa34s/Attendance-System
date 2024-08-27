using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Attendance_System.Models
{
    public class Course
    {
        [Key]
        [Remote("checkid", "Program")]
        public int CrsId { get; set; }
        [Required, StringLength(20)]
      //  [RegularExpression(@"[a-zA-Z]+[a-zA-z0-9]+[a-zA-Z0-9]")]
        // [RegularExpression(@"[a-zA-Z0-9_]+@[a-zA-z]+.[a-zA-Z]{2,4}")]

        public string CrsName { get; set; }
        [Range(10, 1000)]
        public int Duration { get; set; }
        public virtual ICollection<Department> Department { get; set; } = new HashSet<Department>();
        public virtual ICollection<StudentCourse> StudentCourse { get; set; } = new HashSet<StudentCourse>();

    }
}
