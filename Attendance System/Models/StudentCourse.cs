using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_System.Models
{
    public class StudentCourse
    {
        [ForeignKey("Student")]
        public int stId { get; set; }

        [ForeignKey("course")]
        public int crsid { get; set; }

        public int? degree { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [Range(typeof(DateTime), "01/01/2001", "12/31/2099")]
        public DateTime? intake { get; set; }

        public virtual Course course { get; set; }
        public virtual Student Student { get; set; }
    }
}
