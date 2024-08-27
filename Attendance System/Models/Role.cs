namespace Attendance_System.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<UserRole> UserRole { get; set; } = new List<UserRole>();

    }
}
