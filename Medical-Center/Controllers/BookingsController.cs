using Medical_Center_Business.Business;
using Medical_Center_Common.Models.DTO.AppointmentData;
using Medical_Center_Common.Models.DTO.PaymentData;
using Medical_Center_Data.Data;
using Medical_Center_Data.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Center.Controllers
{
    [Route("api/Bookings")]
    [ApiController]
    public class BookingsController:ControllerBase
    {
        private readonly IBookings _bookings;
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _dbUnitOfWork;
        public BookingsController(IBookings bookings, ApplicationDbContext db, IUnitOfWork dbUnitOfWork)
        {
            _bookings = bookings;
            _db = db;
            _dbUnitOfWork = dbUnitOfWork;
        }

        [HttpPost]
        public ActionResult<PaymentDTO> CreateBooking(CreateAppointmentDTO createAppointmentDTO)
        {
            if (createAppointmentDTO == null)
            {
                return BadRequest();
            }

            var appointmentTimeBusy = _db.Appointments.FirstOrDefault(appointment => appointment.AppointmentDateTime == createAppointmentDTO.AppointmentDateTime);

            if (appointmentTimeBusy != null)
            {
                return BadRequest("Appointment Date and Time is not available. Please try a different Date and Time.");
            }

            var booking = _bookings.SaveBookings(createAppointmentDTO);
            return CreatedAtAction(nameof(CreateBooking), new { id = booking.Id }, createAppointmentDTO);
        }
    }
}
