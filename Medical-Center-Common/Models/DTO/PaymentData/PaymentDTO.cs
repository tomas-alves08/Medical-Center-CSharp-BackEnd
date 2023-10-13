using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical_Center_Common.Models.DTO.PaymentData
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public double Price { get; set; }
        public DateTime Created { get; set; }
    }
}
