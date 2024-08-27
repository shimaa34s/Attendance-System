using Attendance_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance_System.Services
{
    public class StudentAttendeceService:IStudentAttendeceService
    {
        ItiDbContext db;
        public StudentAttendeceService(ItiDbContext _db)
        {
            db = _db;
        }
        public StudentAttendece GetById(int id) 
        {
            return db.StudentAttendeces.FirstOrDefault(x => x.Id == id);
        }
        public void Add(StudentAttendece st)
        {
            db.StudentAttendeces.Add(st);
            db.SaveChanges();
        }
        public void Update(StudentAttendece st)
        {
            var existingEntity = db.StudentAttendeces
                .FirstOrDefault(s => s.Id == st.Id);

            if (existingEntity != null)
            {
                if (st.Saturday != existingEntity.Saturday)
                {
                    existingEntity.Saturday = true;
                  //  existingEntity.SelectedDay = "Saturday";
                }

                if (st.Sunday != existingEntity.Sunday)
                {
                    existingEntity.Sunday = true;
                   // existingEntity.SelectedDay = "Sunday";
                }

                if (st.Monday != existingEntity.Monday)
                {
                    existingEntity.Monday = true;
                   // existingEntity.SelectedDay = "Monday";
                }

                if (st.Tuesday != existingEntity.Tuesday)
                {
                    existingEntity.Tuesday = true;
                   // existingEntity.SelectedDay = "Tuesday";

                }

                if (st.Wednesday != existingEntity.Wednesday)
                {
                    existingEntity.Wednesday = true;
                    //existingEntity.SelectedDay = "Wednesday";

                }

                if (st.Thursday != existingEntity.Thursday)
                {
                    existingEntity.Thursday = true;
                  //  existingEntity.SelectedDay = "Thursday";

                }

                if (st.Friday != existingEntity.Friday)
                {
                    existingEntity.Friday = true;
                    //existingEntity.SelectedDay = "Friday";

                }
                db.SaveChanges();
            }
            else
            {
                throw new Exception("Entity not found");
            }
        }




    }
}
