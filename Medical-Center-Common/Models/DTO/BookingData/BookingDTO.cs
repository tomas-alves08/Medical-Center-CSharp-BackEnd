using Medical_Center_Common.Models.DTO.PatientData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_Center_Common.Models.DTO.BookingData
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public bool IsPayed { get; set; } = false;
    }
}
