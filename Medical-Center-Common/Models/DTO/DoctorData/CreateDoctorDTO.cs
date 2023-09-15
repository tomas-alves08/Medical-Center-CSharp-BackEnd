using System.ComponentModel.DataAnnotations;

namespace Medical_Center_Common.Models.DTO.DoctorData
{
    public class CreateDoctorDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ExtraDetails { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int RegistrationNumber { get; set; }
    }
}
