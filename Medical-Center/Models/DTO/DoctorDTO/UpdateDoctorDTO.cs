﻿using System.ComponentModel.DataAnnotations;
using Medical_Center.Data.Models;

namespace Medical_Center.Models.DTO.DoctorDTO
{
    public class UpdateDoctorDTOOutDated
    {
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string ExtraDetails { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        [Required]
        public int RegistrationNumber { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
