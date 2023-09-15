using System.ComponentModel.DataAnnotations;
using Medical_Center_Common.Models.DTO.AppointmentData;

namespace Medical_Center_Common.Models.DTO.PatientData
{
    public class UpdatePatientDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ExtraDetails { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
    }
}
