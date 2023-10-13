using Medical_Center.Data.Models;
using Medical_Center.Data.Repository;
using Medical_Center.Data.Repository.IRepository;

namespace Medical_Center.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private IRepo<Appointment> _appointments;
        private IRepo<Doctor> _doctors;
        private IRepo<Patient> _patients;

        /*private IRepo<Booking> _bookings;*/

        public UnitOfWork(ApplicationDbContext db, 
                            IRepo<Appointment> appointmentRepository, 
                            IRepo<Doctor> doctorsRepository, 
                            IRepo<Patient> patientsRepository
                            /*IRepo<Booking> bookingRepository*/)
        {
             _db = db;
            _appointments = appointmentRepository;
            _doctors = doctorsRepository;
            _patients = patientsRepository;
            /*_bookings = bookingRepository;*/
        }

        public IRepo<Appointment> Appointments => _appointments;

        public IRepo<Doctor> Doctors => _doctors;

        public IRepo<Patient> Patients => _patients;

        /*public IRepo<Payment> Bookings => (IRepo<Payment>)_bookings;*/

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
