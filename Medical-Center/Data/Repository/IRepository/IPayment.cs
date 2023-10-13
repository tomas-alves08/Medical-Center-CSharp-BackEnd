using Medical_Center.Data.Models;

namespace Medical_Center.Data.Repository.IRepository
{
    public interface IPayment
    {
        Task CreateAsync(Appointment appointment, Patient patient);
    }
}
