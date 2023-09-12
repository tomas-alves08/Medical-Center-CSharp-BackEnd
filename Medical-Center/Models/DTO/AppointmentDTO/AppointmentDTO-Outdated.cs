using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Medical_Center.Data.Models;

namespace Medical_Center.Models.DTO.AppointmentDTO
{
    public class AppointmentDTOOutDated
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [JsonIgnore]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        [Required]
        public DateTime AppointmentDateTime { get; set; }
        [Required]
        [JsonIgnore]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string DiagnosisDetails { get; set; }
    }
}
