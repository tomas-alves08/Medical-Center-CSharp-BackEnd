using Medical_Center.Data.Models;
using Medical_Center.Data.Repository.IRepository;

namespace Medical_Center.Business
{
    public interface IAppointmentBusiness
    {
        Task<List<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(int id, bool tracked = true);
        /*Task<Appointment> GetAppointmentByDateAsync(DateTime dateTime, bool tracked = true);*/
        Task CreateAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task RemoveAppointmentAsync(Appointment entity);
    }

    public class AppointmentBusiness : IAppointmentBusiness
    {
        private readonly IRepo<Appointment> _appointmentRepo;
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentBusiness(IRepo<Appointment> appointmentRepo, IUnitOfWork unitOfWork)
        {
            _appointmentRepo = appointmentRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            await _appointmentRepo.CreateAsync(appointment);
            await _unitOfWork.Save();
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepo.GetAllAsync();
        }

        /*public async Task<Appointment> GetAppointmentByDateAsync(DateTime dateTime, bool tracked = true)
        {
            return await _appointmentRepo.GetOneByDateAsync(dateTime, tracked);
        }*/

        public async Task<Appointment> GetAppointmentByIdAsync(int id, bool tracked = true)
        {
            return await _appointmentRepo.GetOneAsync(id, tracked);
        }

        public async Task RemoveAppointmentAsync(Appointment appointment)
        {
            await _appointmentRepo.RemoveAsync(appointment);
            await _unitOfWork.Save();
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            await _appointmentRepo.UpdateAsync(appointment);
            await _unitOfWork.Save();
        }
    }
}
