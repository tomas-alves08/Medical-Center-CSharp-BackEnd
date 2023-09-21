using Medical_Center.Data.Models;
using Medical_Center.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Medical_Center.Data.Repository
{
    public class AppointmentRepository : IRepository<Appointment>
    {
        private readonly ApplicationDbContext _db;

        public AppointmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Appointment entity)
        {
            await _db.Appointments.AddAsync(entity);
            await SaveAsync();
        }

        public List<Appointment> GetAll(Expression<Func<Appointment, bool>> filter = null)
        {
            IQueryable<Appointment> query = _db.Appointments;

            if (filter != null)
            {
               query = query.Where(filter);
            }

            return query.Include(appointment => appointment.Patient)
                        .Include(appointment => appointment.Doctor)
                        .OrderBy(appointment => appointment.AppointmentDateTime)
                        .ToList();
        }

        public Appointment GetOne(Expression<Func<Appointment, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Appointment> query = _db.Appointments;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                query = query
                            .Include(appointment => appointment.Patient)
                            .Include(appointment => appointment.Doctor)
                            .OrderBy(appointment => appointment.AppointmentDateTime)
                            .Where(filter);
            }

            return query.FirstOrDefault();
        }

        public async Task RemoveAsync(Appointment entity)
        {
            _db.Appointments.Remove(entity);
            await SaveAsync();
        }
        public async Task UpdateAsync(Appointment entity)
        {
            _db.Appointments.Update(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
