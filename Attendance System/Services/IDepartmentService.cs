using Attendance_System.Models;

namespace Attendance_System.Services
{
    public interface IDepartmentService
    {
        public List<Department> GetAll();
        public Department GetById(int id);

        public void Delete(int id);
        public void Update(Department std);
        public void Add(Department department);
        public void Save(Department department);
       
    }
}
