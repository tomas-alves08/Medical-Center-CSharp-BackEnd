
namespace Medical_Center_Common.Models.DTO.AppointmentData
{
    public class CreateAppointmentDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
    }
}
