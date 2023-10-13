using Medical_Center_Common.Models.DTO.PatientData;

namespace Medical_Center.Data.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public Patient? Patient { get; set; }
        public int PatientId { get; set; }
        public Appointment? Appointment { get; set; }
        public int AppointmentId { get; set; }
        public bool IsPayed { get; set; } = false;
    }
}
