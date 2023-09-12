using System.ComponentModel.DataAnnotations;
using Medical_Center.Data.Models;
using Medical_Center.Models;

namespace Medical_Center_Common.Models.DTO.PatientData
{
    public class PatientDTO
    {
        [Required]
        public int PatientId { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
