using Medical_Center_Common.Models.DTO.AppointmentData;
using System.Collections.ObjectModel;

namespace Medical_Center.Data.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ExtraDetails { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int RegistrationNumber { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public Doctor()
        {
            Appointments = new Collection<Appointment>();
        }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
