﻿using Medical_Center_Common.Models.DTO.DoctorData;
using Medical_Center_Common.Models.DTO.PatientData;

namespace Medical_Center_Common.Models.DTO.AppointmentData
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public PatientDTO? PatientDTO { get; set; }

        public DateTime AppointmentDateTime { get; set; }
        public int DoctorId { get; set; }
        public DoctorDTO? DoctorDTO { get; set; }
        public string DiagnosisDetails { get; set; } = string.Empty;
    }
}
