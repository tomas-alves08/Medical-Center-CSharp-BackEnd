namespace Medical_Center.Data.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public double Price { get; set; }
        public DateTime Created { get; set; }
    }
}
