using Microsoft.EntityFrameworkCore;

namespace Attendance_System.Models
{
    public class ItiDbContext:DbContext
    {
        public ItiDbContext(DbContextOptions options) :base(options)
        {
            
        }
        public ItiDbContext()
        {
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourse { get; set; }
        public DbSet<Hr> Hrs { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<StudentAttendece> StudentAttendeces { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies()
                .UseSqlServer();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>(s =>
            {
                s.HasKey(a => new { a.UId, a.RId });
            });
            modelBuilder.Entity<StudentCourse>(s =>
            {
                s.HasKey(a => new { a.crsid, a.stId });
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
