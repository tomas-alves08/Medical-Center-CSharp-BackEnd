using Medical_Center.Data.Models;
using Medical_Center.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Medical_Center.Data.Repository
{
    public class DoctorRepository : IRepository<Doctor>
    {
        private readonly ApplicationDbContext _db;

        public DoctorRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Doctor entity)
        {
            await _db.Doctors.AddAsync(entity);
            await SaveAsync();
        }

        public List<Doctor> GetAll(Expression<Func<Doctor, bool>> filter = null)
        {
            IQueryable<Doctor> query = _db.Doctors;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            return query.Include(doc => doc.Appointments)
                        .OrderBy(doc => doc.Id)
                        .ToList();
        }

        public Doctor GetOne(Expression<Func<Doctor, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Doctor> query = _db.Doctors;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if(filter != null)
            {
                query = query.Include(doc => doc.Appointments)
                             .OrderBy(doc => doc.Id)
                             .Where(filter);
            }

            return query.FirstOrDefault();
        }

        public async Task RemoveAsync(Doctor entity)
        {
            _db.Doctors.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor entity)
        {
            _db.Doctors.Update(entity);
            await SaveAsync();
        }
    }
}
