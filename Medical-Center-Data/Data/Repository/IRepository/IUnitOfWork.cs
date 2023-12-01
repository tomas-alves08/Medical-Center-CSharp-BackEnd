using Medical_Center_Data.Data.Models;

namespace Medical_Center_Data.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepo<Appointment> Appointments { get; }
        IRepo<Doctor> Doctors { get; }
        IRepo<Patient> Patients { get; }
        Task Save();
    }
}
