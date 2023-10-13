using Medical_Center.Data.Models;

namespace Medical_Center.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepo<Appointment> Appointments { get; }
        IRepo<Doctor> Doctors { get; }
        IRepo<Patient> Patients { get; }
        Task Save();
    }
}
