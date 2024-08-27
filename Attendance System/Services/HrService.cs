using Attendance_System.Models;

namespace Attendance_System.Services
{
    public class HrService:IHrService
    {
        ItiDbContext db;
        public HrService(ItiDbContext _db)
        {
            db = _db;   
        }
        public List<Hr> GetAll()
        {
            return db.Hrs.ToList();
        }
        public void Add(Hr hr)
        {
            db.Hrs.Add(hr);
            db.SaveChanges();
        }

        public void Update(Hr hr)
        {
            db.Hrs.Update(hr);
            db.SaveChanges();
        }
        public void Delete(Hr hr)
        {
            db.Hrs.Remove(hr);
            db.SaveChanges();
        }
        public Hr GetById(int id)
        {
            return db.Hrs.FirstOrDefault(h => h.Id == id);
        }
        public Hr Getbyemail(Hr hr)
        {
            return db.Hrs.Where(s => s.Email == hr.Email && s.Id != hr.Id).FirstOrDefault();
        }
        public Hr GetbyemailAndName(User user)
        {
           // Hr res=new Hr() { };
           return db.Hrs.Where(s=>s.Name==user.UserName&&s.Password==user.Password).FirstOrDefault();
        }
        public Hr validate(string email, int id)
        {
            return db.Hrs.Where(s => s.Email == email && s.Id != id).FirstOrDefault();
        }

    }
}
