using AutoMapper;
using Medical_Center_Common.Models.DTO.AppointmentData;
using Medical_Center_Common.Models.DTO.DoctorData;
using Medical_Center_Common.Models.DTO.PatientData;
using Medical_Center_Common.Models.DTO.PaymentData;
using Medical_Center_Common.Models.DTO.UserData.LocalUserData;
using Medical_Center_Common.Models.DTO.UserData.LoginData;
using Medical_Center_Common.Models.DTO.UserData.RegistrationData;
using Medical_Center_Data.Data.Models;

namespace Medical_Center
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
            CreateMap<Appointment, CreateAppointmentDTO>().ReverseMap();
            CreateMap<Appointment, UpdateAppointmentDTO>().ReverseMap();

            CreateMap<Patient, PatientDTO>().ReverseMap();
            CreateMap<Patient, CreatePatientDTO>().ReverseMap();
            CreateMap<Patient, UpdatePatientDTO>().ReverseMap();

            CreateMap<Doctor, DoctorDTO>().ReverseMap();
            CreateMap<Doctor, CreateDoctorDTO>().ReverseMap();
            CreateMap<Doctor, UpdateDoctorDTO>().ReverseMap();

            CreateMap<Payment, PaymentDTO>().ReverseMap();

            CreateMap<LocalUser, RegistrationRequestDTO>().ReverseMap();
            CreateMap<LocalUserDTO, LoginRequestDTO>().ReverseMap();
            CreateMap<LocalUser, LocalUserDTO>().ReverseMap();
        }
    }
}
