using System.ComponentModel.DataAnnotations;

namespace Medical_Center.Models.DTO.AppointmentDTO
{
    public class UpdateAppointmentDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int PatientId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        [Required]
        public int DoctorId { get; set; }
        public string DiagnosisDetails { get; set; }
    }
}
