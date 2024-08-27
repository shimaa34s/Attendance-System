using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Attendance_System.Models
{
    public class Department
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Remote("checkid", "department")]

        public int DeptId { get; set; }
        [Required, StringLength(20)]
        public string DeptName { get; set; }
        [Range(10, 1000)]

        public int Capacity { get; set; }
        public bool status { get; set; }

        public virtual ICollection<Student> student { get; set; } = new HashSet<Student>();
        public virtual ICollection<Course> Course { get; set; } = new HashSet<Course>();

    }
}
