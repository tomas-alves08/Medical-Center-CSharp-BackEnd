using Medical_Center.Data.Models;
using Medical_Center.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;/*
using System.Linq.Expressions;*/

namespace Medical_Center.Data.Repository
{
    public class PatientRepository : IRepo<Patient>
    {
        private readonly ApplicationDbContext _db;

        public PatientRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Patient entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            IQueryable<Patient> query = _db.Patients;

            var result = await query.Include(patient => patient.Appointments)
                                    .OrderBy(patient => patient.FirstName)
                                    .ToListAsync();

            return result;
        }

        public async Task<Patient> GetOneAsync(int id, bool tracked = true)
        {
            IQueryable<Patient> query = _db.Patients;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            var result = await query.Include(patient => patient.Appointments)
                                    .OrderBy(patient => patient.Id)
                                    .FirstOrDefaultAsync(doc => doc.Id == id);

            return result;
        }

        public async Task RemoveAsync(Patient entity)
        {
            _db.Remove(entity);
        }

        public async Task UpdateAsync(Patient entity)
        {
            _db.Update(entity);
        }
    }
}
