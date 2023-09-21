using Medical_Center.Data.Models;
using Medical_Center.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Medical_Center.Data.Repository
{
    public class PatientRepository : IRepository<Patient>
    {
        private readonly ApplicationDbContext _db;

        public PatientRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Patient entity)
        {
            await _db.AddAsync(entity);
            await SaveAsync();
        }

        public List<Patient> GetAll(Expression<Func<Patient, bool>> filter = null)
        {
            IQueryable<Patient> query = _db.Patients;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            return query.Include(patient => patient.Appointments)
                        .OrderBy(patient => patient.FirstName)
                        .ToList();
        }

        public Patient GetOne(Expression<Func<Patient, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Patient> query = _db.Patients;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            if(filter != null)
            {
                query = query.Include(patient => patient.Appointments)
                             .OrderBy(patient => patient.Id)
                             .Where(filter);
            }

            return query.FirstOrDefault();
        }

        public async Task RemoveAsync(Patient entity)
        {
            _db.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patient entity)
        {
            _db.Update(entity);
            await SaveAsync();
        }
    }
}
