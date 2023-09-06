using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Medical_Center.Models
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string ExtraDetails { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        [Required]
        public int RegistrationNumber { get; set; }
        [JsonIgnore]
        public ICollection<Appointment>? Appointments { get; set; }
        public Doctor()
        {
            Appointments = new Collection<Appointment>();
        }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
