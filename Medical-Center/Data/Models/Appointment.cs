﻿
namespace Medical_Center.Data.Models
{
    public class Appointment
    {
        /*[DatabaseGenerated(DatabaseGeneratedOption.Identity)]*/
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
        public DateTime AppointmentDateTime { get; set; } 
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public string DiagnosisDetails { get; set; } = string.Empty;
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}