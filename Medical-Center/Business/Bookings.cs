using Medical_Center.Data.Models;
using Medical_Center.Data.Repository.IRepository;
using Medical_Center_Common.Models.DTO.AppointmentData;
using System.Transactions;
using AutoMapper;
using Medical_Center_Common.Models.DTO.PaymentData;

namespace Medical_Center.Business
{
    public interface IBookings
    {
        PaymentDTO SaveBookings(AppointmentDTO appointment);
    }
    public class Bookings : IBookings
    {
        private readonly IRepo<Appointment> _appointments;
        private readonly IRepo<Payment> _bookings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Bookings(IRepo<Appointment> appointmentRepository, 
                        IRepo<Payment> bookingRepository, 
                        IUnitOfWork unitOfWork, 
                        IMapper mapper
                       )
        {
            _appointments = appointmentRepository;
            _bookings = bookingRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public PaymentDTO SaveBookings(AppointmentDTO appointmentDTO) 
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    DateTime todayNow = DateTime.Now;
                    TimeOnly now = new TimeOnly(todayNow.TimeOfDay.Hours);
                    TimeOnly time10pm = new TimeOnly(22,0,0);

                    if(now > time10pm){
                        throw new Exception("Payments not allowed after 10pm. Please try again tomorrow.");
                    }

                    Payment payment = new()
                    {
                        PatientId=appointmentDTO.PatientId,
                        //Todo Price Table and replace hardcoded price below
                        Price= 100,
                        Created = DateTime.Now
                    };

                    Appointment appointment = _mapper.Map<Appointment>(appointmentDTO);
                    PaymentDTO paymentDTO = _mapper.Map<PaymentDTO>(payment);

                    _appointments.CreateAsync(appointment);
                    payment.AppointmentId = appointmentDTO.Id;
                    _bookings.CreateAsync(payment);
                    _unitOfWork.Save();
                    scope.Complete();

                    return paymentDTO;
                } 
                catch (Exception ex)
                {
                    // TODO Log Error!
                    return null;
                }
            }
        }
    }
}
