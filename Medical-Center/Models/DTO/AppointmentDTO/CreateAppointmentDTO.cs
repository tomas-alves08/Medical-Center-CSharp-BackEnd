using System.ComponentModel.DataAnnotations;

namespace Medical_Center.Models.DTO.AppointmentDTO
{
    public class CreateAppointmentDTO
    {
        [Required]
        public int PatientId { get; set; }
        [Required]
        public DateTime AppointmentDateTime { get; set; }
        [Required]
        public int DoctorId { get; set; }
    }
}
