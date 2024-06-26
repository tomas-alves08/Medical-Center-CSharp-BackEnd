﻿using Medical_Center_Data.Data.Models;
using Medical_Center_Data.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Medical_Center_Data.Data.Repository
{
    public class AppointmentRepository : IRepo<Appointment>
    {
        private readonly ApplicationDbContext _db;

        public AppointmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Appointment entity)
        {
            await _db.Appointments.AddAsync(entity);
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            IQueryable<Appointment> query = _db.Appointments;

            var result = await query.Include(appointment => appointment.Patient)
                                    .Include(appointment => appointment.Doctor)
                                    .OrderBy(appointment => appointment.Id)
                                    .ToListAsync();

            return result;
        }

        public async Task<Appointment> GetOneAsync(int id, bool tracked = true)
        {
            IQueryable<Appointment> query = _db.Appointments;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            var result = await query.Include(appointment => appointment.Patient)
                                    .Include(appointment => appointment.Doctor)
                                    .OrderBy(appointment => appointment.AppointmentDateTime)
                                    .FirstOrDefaultAsync(appointment => appointment.Id == id);

            return result;
        }

        public async Task<Appointment> GetOneByDateAsync(DateTime dateTime, bool tracked = true)
        {
            IQueryable<Appointment> query = _db.Appointments;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }

            var result = await query.Include(appointment => appointment.Patient)
                                    .Include(appointment => appointment.Doctor)
                                    .FirstOrDefaultAsync(appointment => appointment.AppointmentDateTime == dateTime);

            return result;
        }

        public Task RemoveAsync(Appointment entity)
        {
            _db.Appointments.Remove(entity);
            return Task.CompletedTask;
        }
        public Task UpdateAsync(Appointment entity)
        {
            _db.Appointments.Update(entity);
            return Task.CompletedTask;
        }
    }
}
