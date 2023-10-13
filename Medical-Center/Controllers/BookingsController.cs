using Medical_Center.Business;
using Medical_Center.Data.Models;
using Medical_Center_Common.Models.DTO.AppointmentData;
using Medical_Center_Common.Models.DTO.BookingData;
using Medical_Center_Common.Models.DTO.PaymentData;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Center.Controllers
{
    [Route("api/Bookings")]
    [ApiController]
    public class BookingsController
    {
        private readonly IBookings _bookings;
        public BookingsController(IBookings bookings)
        {
            _bookings = bookings;
        }

        [HttpPost]
        public /*ActionResult<PaymentDTO>*/ void CreateBooking(AppointmentDTO createAppointmentDTO)
        {
            if (createAppointmentDTO == null)
            {
                /*return BadRequest();*/
            }

            PaymentDTO payment = new()
            {
                PatientId = createAppointmentDTO.PatientId,
                //Todo Price Table and replace hardcoded price below
                Price = 100,
                Created = DateTime.Now
            };

            var booking = _bookings.SaveBookings(createAppointmentDTO);
            /*return CreatedAtAction(nameof(CreateBooking), new { id = booking.Id }, payment);*/
        }
    }
}
