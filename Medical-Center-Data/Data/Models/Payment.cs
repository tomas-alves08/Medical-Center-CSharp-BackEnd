﻿namespace Medical_Center_Data.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public double Price { get; set; }
        public bool IsPayed { get; set; } = false;
        public DateTime Created { get; set; }
    }
}
