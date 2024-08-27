using Attendance_System.Models;

namespace Attendance_System.Services
{
    public interface IStudentService
    {
        public List<Student> GetAll();
        public Student GetById(int id);
        public Student GetByemail(string email);
        public void Delete(int id);
        public void Update(Student std);
        public void Add(Student student);
        public Student ValidateEmail2(Student student);
        public Student ValidateEmailst(string email, int id);
        public List<Student> GetAllStData();
        public List<Student> GetHrDept();
        public Student ValidateEmail(string email);


        public Student ValidateEmail(Student student);
        public Student GetByUserNPass(string email, string password);
        public List<Student> GetByHr(int hnum);
        public Student ValidateName(Student student);
        public Student Validate(string email,int id);

    }
}
