﻿namespace Medical_Center_Common.Models.DTO.AppointmentData
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int PaymentId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string DiagnosisDetails { get; set; } = string.Empty;
    }
}
