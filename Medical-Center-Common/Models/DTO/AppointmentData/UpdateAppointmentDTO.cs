
namespace Medical_Center_Common.Models.DTO.AppointmentData
{
    public class UpdateAppointmentDTO
    {
        
        public int Id { get; set; }
        
        public int PatientId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public int DoctorId { get; set; }
        public string DiagnosisDetails { get; set; } = string.Empty;
    }
}
