using Medical_Center_Data.Data.Models;
using Medical_Center_Data.Data.Repository.IRepository;
using Medical_Center_Common.Models.DTO.AppointmentData;
using AutoMapper;
using Medical_Center_Common.Models.DTO.PaymentData;
using Medical_Center_Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Medical_Center_Business.Business
{
    public interface IBookings
    {
        PaymentDTO SaveBookings(CreateAppointmentDTO appointment);
    }
    public class Bookings : ControllerBase, IBookings
    {
        private readonly IRepo<Appointment> _appointments;
        private readonly IRepo<Payment> _bookings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<Bookings> _logger;
        private readonly ApplicationDbContext _db;

        public Bookings(IRepo<Appointment> appointmentRepository, 
                        IRepo<Payment> bookingRepository, 
                        IUnitOfWork unitOfWork, 
                        IMapper mapper,
                        ILogger<Bookings> logger,
                        ApplicationDbContext db
                       )
        {
            _appointments = appointmentRepository;
            _bookings = bookingRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _db = db;
        }

        public PaymentDTO SaveBookings(CreateAppointmentDTO appointmentDTO) 
        {
            /*using (TransactionScope scope = new TransactionScope())
            {*/
                try
                {
                    var patient = _unitOfWork.Patients.GetOneAsync(appointmentDTO.PatientId);
                    var doctor = _unitOfWork.Doctors.GetOneAsync(appointmentDTO.DoctorId);

                    if ( patient == null)
                    {  
                        _logger.LogInformation("Please enter a valid Patient ID.");
                        return null;
                    }

                    if (doctor == null)
                    {
                        _logger.LogInformation("Please enter a valid Doctor ID.");
                        return null;
                    }

                    DateTime todayNow = DateTime.Now;
                    TimeOnly now = TimeOnly.FromDateTime(todayNow);
                    TimeOnly time10pm = new TimeOnly(22,0,0);

                    if (now >= time10pm)
                    {
                        throw new Exception("Payments not allowed after 10pm. Please try again tomorrow.");
                    }

                Payment payment = new()
                    {
                        PatientId = appointmentDTO.PatientId,
                        Price= 100,
                        IsPayed= true,
                        Created = DateTime.Now
                    };

                        // Business Pricing Rules
                        TimeOnly time7am = new TimeOnly(7, 0, 0);
                        TimeOnly time10am = new TimeOnly(10, 0, 0);

                        TimeOnly time5pm = new TimeOnly(17,0,0);
                        TimeOnly time8pm = new TimeOnly(20,0,0);

                        TimeOnly appointmentTime = TimeOnly.FromDateTime(appointmentDTO.AppointmentDateTime);

                        if(appointmentTime >= time7am && appointmentTime <= time10am)
                        {
                            payment.Price = 125;
                        }

                        if(appointmentTime >= time5pm && appointmentTime <= time8pm) 
                        {
                            payment.Price = 140;
                        }

                    Appointment appointment = _mapper.Map<Appointment>(appointmentDTO);
                    PaymentDTO paymentDTO = _mapper.Map<PaymentDTO>(payment);

                var newAppointment = _unitOfWork.Appointments.CreateAsync(appointment);
                payment.AppointmentId = newAppointment.Id;
                var booking = _bookings.CreateAsync(payment);
                appointment.PaymentId = booking.Id;
                Console.WriteLine(_db.Entry(appointment).State);
                _unitOfWork.Save();
                /*scope.Complete();*/

                return paymentDTO;
            } 
                catch (Exception)
                {
                    _logger.LogInformation("Failed when trying to create a booking.");
                    return null;
                }
            /*}*/
        }

        
    }
}
