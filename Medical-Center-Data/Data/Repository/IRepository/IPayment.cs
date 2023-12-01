using Medical_Center_Data.Data.Models;

namespace Medical_Center_Data.Data.Repository.IRepository
{
    public interface IPayment
    {
        Task CreateAsync(Appointment appointment, Patient patient);
    }
}
