using Medical_Center_Data.Data.Models;
using Medical_Center_Data.Data.Repository.IRepository;

namespace Medical_Center_Data.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private IRepo<Appointment> _appointments;
        private IRepo<Doctor> _doctors;
        private IRepo<Patient> _patients;

        private IRepo<Payment> _bookings;
        private ApplicationDbContext db;

        public UnitOfWork(ApplicationDbContext db, 
                            IRepo<Appointment> appointmentRepository, 
                            IRepo<Doctor> doctorsRepository, 
                            IRepo<Patient> patientsRepository,
                            IRepo<Payment> bookingRepository)
        {
             _db = db;
            _appointments = appointmentRepository;
            _doctors = doctorsRepository;
            _patients = patientsRepository;
            _bookings = bookingRepository;
        }

        public IRepo<Appointment> Appointments => _appointments;

        public IRepo<Doctor> Doctors => _doctors;

        public IRepo<Patient> Patients => _patients;

        public IRepo<Payment> Bookings => _bookings;

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
