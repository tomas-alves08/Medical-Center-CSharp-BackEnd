using AutoMapper;
using Medical_Center.Data.Models;
using Medical_Center.Models.DTO.AppointmentDTO;
using Medical_Center.Models.DTO.DoctorDTO;
using Medical_Center.Models.DTO.PatientDTO;

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
        }
    }
}
