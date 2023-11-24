using Medical_Center_Data.Data.Models;
using Medical_Center_Data.Data.Repository.IRepository;

namespace Medical_Center_Business.Business
{
    public interface IPatientBusiness
    {
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient> GetOnePatientAsync(int id, bool tracked = true);
        Task CreatePatientAsync(Patient patient);
        Task RemovePatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
    }
    public class PatientBusiness : IPatientBusiness
    {
        private readonly IRepo<Patient> _patientRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PatientBusiness(IRepo<Patient> patientRepo, IUnitOfWork unitOfWork)
        {
            _patientRepo = patientRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task CreatePatientAsync(Patient patient)
        {
            await _patientRepo.CreateAsync(patient);
            await _unitOfWork.Save();
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepo.GetAllAsync();
        }

        public async Task<Patient> GetOnePatientAsync(int id, bool tracked = true)
        {
            return await _patientRepo.GetOneAsync(id, tracked);
        }

        public async Task RemovePatientAsync(Patient patient)
        {
            await _patientRepo.RemoveAsync(patient);
            await _unitOfWork.Save();
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            await _patientRepo.UpdateAsync(patient);
            await _unitOfWork.Save();
        }
    }
}
