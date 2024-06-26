﻿using Medical_Center_Data.Data.Models;
using Medical_Center_Data.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Medical_Center_Data.Data.Repository
{
    public class DoctorRepository : IRepo<Doctor>
    {
        private readonly ApplicationDbContext _db;

        public DoctorRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Doctor entity)
        {
            await _db.Doctors.AddAsync(entity);
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            IQueryable<Doctor> query = _db.Doctors;

            var result = await query.Include(doc => doc.Appointments)
                                    .OrderBy(doc => doc.Id)
                                    .ToListAsync();

            return result;
        }

        public async Task<Doctor> GetOneAsync(int id, bool tracked = true)
        {
            IQueryable<Doctor> query = _db.Doctors;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            var result = await query
                                    .Include(doc => doc.Appointments)
                                    .OrderBy(doc => doc.Id)
                                    .FirstOrDefaultAsync(doc => doc.Id == id);

            return result;
        }

        public async Task<Doctor> GetOneByRegistrationNumberAsync(int registrationNumber, bool tracked = true)
        {
            IQueryable<Doctor> query = _db.Doctors;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            var result = await query
                                    .Include(doc => doc.Appointments)
                                    .OrderBy(doc => doc.Id)
                                    .FirstOrDefaultAsync(doc => doc.RegistrationNumber == registrationNumber);

            return result;
        }

        public async Task RemoveAsync(Doctor entity)
        {
            _db.Doctors.Remove(entity);
        }

        public async Task UpdateAsync(Doctor entity)
        {
            _db.Doctors.Update(entity);
        }
    }
}
