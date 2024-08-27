using Attendance_System.Models;
namespace Attendance_System.Services
{
    public interface IHrService
    {
        public List<Hr> GetAll();
        public void Add(Hr hr);
        public void Update(Hr hr);
        public void Delete(Hr hr);
        public Hr GetById(int id);
        public Hr Getbyemail(Hr hr);
        public Hr GetbyemailAndName(User user );


        public Hr validate(string email,int id);
    }
}
