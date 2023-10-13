using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_Center_Common.Models.DTO.PaymentData
{
    internal class AppointmentOrderResponseDTO
    {
        public string? SessionId { get; set; }
        public string? PubKey { get; set; }
    }
}
