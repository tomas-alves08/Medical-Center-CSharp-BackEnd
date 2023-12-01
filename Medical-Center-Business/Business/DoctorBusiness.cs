using Medical_Center_Data.Data.Models;
using Medical_Center_Data.Data.Repository.IRepository;

namespace Medical_Center_Business.Business
{
    public interface IDoctorBusiness
    {
        Task<List<Doctor>> GetAlldoctorsAsync();
        Task<Doctor> GetOnedoctorAsync(int id, bool tracked = true);
        /*Task<Doctor> GetOneDoctorByRegistrationNumberAsync(int registrationNumber, bool tracked = true);*/
        Task CreateDoctorAsync(Doctor doctor);
        Task RemoveDoctorAsync(Doctor doctor);
        Task UpdateDoctorAsync(Doctor doctor);
    }
    public class DoctorBusiness : IDoctorBusiness
    {
        private readonly IRepo<Doctor> _doctorRepo;
        private readonly IUnitOfWork _unitOfWork;

        public DoctorBusiness(IRepo<Doctor> doctorRepo, IUnitOfWork unitOfWork)
        {
            _doctorRepo = doctorRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateDoctorAsync(Doctor doctor)
        {
            await _doctorRepo.CreateAsync(doctor);
            await _unitOfWork.Save();
        }

        public async Task<List<Doctor>> GetAlldoctorsAsync()
        {
            return await _doctorRepo.GetAllAsync();
        }

        public async Task<Doctor> GetOnedoctorAsync(int id, bool tracked = true)
        {
            return await _doctorRepo.GetOneAsync(id, tracked);
        }

       /* public async Task<Doctor> GetOneDoctorByRegistrationNumberAsync(int registrationNumber, bool tracked = true)
        {
            return await _doctorRepo.GetOneByRegistrationNumberAsync(registrationNumber, tracked);
        }*/

        public async Task RemoveDoctorAsync(Doctor doctor)
        {
            await _doctorRepo.RemoveAsync(doctor);
            await _unitOfWork.Save();
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            await _doctorRepo.UpdateAsync(doctor);
            await _unitOfWork.Save();
        }
    }
}
