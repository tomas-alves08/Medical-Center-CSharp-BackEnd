using DocuSign.eSign.Model;
using Medical_Center_Business.Business;
using Medical_Center_Common.Models.DTO.AppointmentData;
using Medical_Center_Common.Models.DTO.EmailData;
using Medical_Center_Common.Models.DTO.PaymentData;
using Medical_Center_Data.Data;
using Medical_Center_Data.Data.Repository.IRepository;
using Medical_Center_Services.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IPatientBusiness _patientRepo;
        private readonly IDoctorBusiness _doctorRepo;
        private readonly IEmailService _emailService;
        public BookingsController(IBookings bookings, ApplicationDbContext db, IUnitOfWork dbUnitOfWork, IPatientBusiness patientRepo, IDoctorBusiness doctorRepo
            ,IEmailService emailService)
        {
            _bookings = bookings;
            _db = db;
            _dbUnitOfWork = dbUnitOfWork;
            _patientRepo = patientRepo;
            _doctorRepo = doctorRepo;
            _emailService = emailService;
        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public async Task<ActionResult<PaymentDTO>> CreateBooking(CreateAppointmentDTO createAppointmentDTO)
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

            var patient = await _patientRepo.GetOnePatientAsync(createAppointmentDTO.PatientId);
            var doctor = await _doctorRepo.GetOnedoctorAsync(createAppointmentDTO.DoctorId);

            if (patient == null || doctor == null)
            {
                return BadRequest("Either Doctor or Patient is not valid.");
            }

            var booking = _bookings.SaveBookings(createAppointmentDTO);

            EmailDTO email = new EmailDTO()
            {
                To = "ta3117362@gmail.com",
                Subject = "Booking appointment created Succesfully",
                Body = $@"Hello Mr./Mrs. {patient.FirstName} {patient.LastName},
                            
                            Your appointment has been booked succesfully at {createAppointmentDTO.AppointmentDateTime} with Dr. {doctor.FirstName} {doctor.LastName}.

                            Sincerely,

                            Medical Center"
            };

            _emailService.SendEmail(email);

            return CreatedAtAction(nameof(CreateBooking), new { id = booking.Id }, createAppointmentDTO);
        }
    }
}
