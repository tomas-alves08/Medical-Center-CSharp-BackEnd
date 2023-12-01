using Medical_Center_Common.Models.DTO.AppointmentData;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Medical_Center_Data.Data.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ExtraDetails { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public Patient()
        {
            Appointments = new Collection<Appointment>();
        }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
