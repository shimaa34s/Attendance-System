using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_System.Models
{
   
    public class StudentAttendece
    {
        [ForeignKey("student")]
        public int Id { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        [NotMapped]
        public string SelectedDay { get; set; }

        public virtual Student student { get; set; }
    }
}
