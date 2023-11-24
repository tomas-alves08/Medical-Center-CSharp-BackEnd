using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.EntityFrameworkCore;
using Medical_Center_Business.Business;
using Medical_Center_Data.Data.Repository.IRepository;
using Medical_Center_Data.Data.Models;
using Medical_Center_Data.Data;
using Medical_Center;
using Medical_Center_Common.Models.DTO.AppointmentData;

namespace Medical_Center_Test.Business
{
    public class BookingsTests
    {
        private readonly Mock<ILogger<Bookings>> _logger;
        private readonly Mock<IRepo<Appointment>> _appointmentRepository;
        private readonly Mock<IRepo<Payment>> _paymentRepository;
        private readonly ApplicationDbContext _db;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private Bookings _bookings;

        public BookingsTests()
        {
            _appointmentRepository = new Mock<IRepo<Appointment>>();
            _paymentRepository = new Mock<IRepo<Payment>>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _logger = new Mock<ILogger<Bookings>>();

            MapperConfiguration mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingConfig()); }); 
            IMapper mapper = new Mapper(mapperConfig);

            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BookingsTests")
                /*.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))*/
                .Options;

            _db = new ApplicationDbContext(dbContextOptions);

            var bookings = new Bookings(_appointmentRepository.Object,
                                        _paymentRepository.Object, 
                                        _unitOfWork.Object, 
                                        mapper, 
                                        _logger.Object, 
                                        _db);

            _bookings = bookings;
        }

        [Fact] 
        public void GivenBookings_WhenAppointmentIsProvided_ThenCreatePayment() 
        {
            //Arrange
            CreateAppointmentDTO appointmentDTO = new CreateAppointmentDTO
            {
                PatientId = 7,
                DoctorId = 7,
                AppointmentDateTime = DateTime.Now
            };

            _unitOfWork.Setup(x => x.Appointments.CreateAsync(It.IsAny<Appointment>()));

            _unitOfWork.Setup(uow => uow.Patients.GetOneAsync(7, true)).ReturnsAsync(new Patient
            {
                Id = 7,
                FirstName = "Pedro",
                LastName = "Ribeiro",
                ExtraDetails = "has got a swollen right wrist",
                Address = "Mount Vic road",
                City = "Wellington",
                BirthDate = new DateTime(1992, 05, 19)
            });

            _unitOfWork.Setup(uow => uow.Doctors.GetOneAsync(7, true)).ReturnsAsync(new Doctor
            {
                Id = 7,
                FirstName = "Ronaldo",
                LastName = "Fenomeno",
                ExtraDetails = "Joga Muito",
                Address = "Test",
                City = "Wellington",
                RegistrationNumber = 9999
            });

            //Act
            var result = _bookings.SaveBookings(appointmentDTO);

            //Assert
            Assert.NotNull(result);

            _unitOfWork.Verify(x => x.Appointments.CreateAsync(It.IsAny<Appointment>()), Times.Once());
        }

        [Fact]
        public void GivenBookings_WhenAppointmentIsMadeAfter10pm_ThenAppointmentForbidden()
        {
            //Arrange
            TimeOnly now = new TimeOnly(22, 00, 00);

            CreateAppointmentDTO appointmentDTO = new CreateAppointmentDTO
            {
                PatientId = 7,
                DoctorId = 7,
                AppointmentDateTime = DateTime.Now
            };

            _unitOfWork.Setup(x => x.Appointments.CreateAsync(It.IsAny<Appointment>()));

            _unitOfWork.Setup(uow => uow.Patients.GetOneAsync(7, true)).ReturnsAsync(new Patient
            {
                Id = 7,
                FirstName = "Pedro",
                LastName = "Ribeiro",
                ExtraDetails = "has got a swollen right wrist",
                Address = "Mount Vic road",
                City = "Wellington",
                BirthDate = new DateTime(1992, 05, 19)
            });

            _unitOfWork.Setup(uow => uow.Doctors.GetOneAsync(7, true)).ReturnsAsync(new Doctor
            {
                Id = 7,
                FirstName = "Ronaldo",
                LastName = "Fenomeno",
                ExtraDetails = "Joga Muito",
                Address = "Test",
                City = "Wellington",
                RegistrationNumber = 9999
            });

            //Act
            var result = _bookings.SaveBookings(appointmentDTO);

            //Assert
            Assert.Null(appointmentDTO);

            _unitOfWork.Verify(x => x.Appointments.CreateAsync(It.IsAny<Appointment>()), Times.Once());
        }

        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void GivenBookings_WhenAppointmentIsEarlyMorning_ThenAdjustPrice(int time)
        {
            //Arrange
            DateTime now = DateTime.Now;
            TimeOnly appointmentTime = new TimeOnly(time, 0, 0);
            DateTime appointmentDateTime = now.Date.Add(appointmentTime.ToTimeSpan());
            int earlyMorningAppointmentPrice = 125;

            CreateAppointmentDTO appointmentDTO = new CreateAppointmentDTO
            {
                PatientId = 7,
                DoctorId = 7,
                AppointmentDateTime = appointmentDateTime
            };

            _unitOfWork.Setup(x => x.Appointments.CreateAsync(It.IsAny<Appointment>()));

            _unitOfWork.Setup(uow => uow.Patients.GetOneAsync(7, true)).ReturnsAsync(new Patient
            {
                Id = 7,
                FirstName = "Pedro",
                LastName = "Ribeiro",
                ExtraDetails = "has got a swollen right wrist",
                Address = "Mount Vic road",
                City = "Wellington",
                BirthDate = new DateTime(1992, 05, 19)
            });

            _unitOfWork.Setup(uow => uow.Doctors.GetOneAsync(7, true)).ReturnsAsync(new Doctor
            {
                Id = 7,
                FirstName = "Ronaldo",
                LastName = "Fenomeno",
                ExtraDetails = "Joga Muito",
                Address = "Test",
                City = "Wellington",
                RegistrationNumber = 9999
            });

            //Act
            var result = _bookings.SaveBookings(appointmentDTO);

            

            //Assert
            Assert.Equal(earlyMorningAppointmentPrice, result.Price);

            _unitOfWork.Verify(x => x.Appointments.CreateAsync(It.IsAny<Appointment>()), Times.Once());
        }

        [Theory]
        [InlineData(17)]
        [InlineData(18)]
        [InlineData(19)]
        [InlineData(20)]
        public void GivenBookings_WhenAppointmentIsLateAfternoon_ThenAdjustPrice(int time)
        {
            //Arrange
            DateTime now = DateTime.Now;
            TimeOnly appointmentTime = new TimeOnly(time, 0, 0);
            DateTime appointmentDateTime = now.Date.Add(appointmentTime.ToTimeSpan());
            int earlyMorningAppointmentPrice = 140;

            CreateAppointmentDTO appointmentDTO = new CreateAppointmentDTO
            {
                PatientId = 7,
                DoctorId = 7,
                AppointmentDateTime = appointmentDateTime
            };

            _unitOfWork.Setup(x => x.Appointments.CreateAsync(It.IsAny<Appointment>()));

            _unitOfWork.Setup(uow => uow.Patients.GetOneAsync(7, true)).ReturnsAsync(new Patient
            {
                Id = 7,
                FirstName = "Pedro",
                LastName = "Ribeiro",
                ExtraDetails = "has got a swollen right wrist",
                Address = "Mount Vic road",
                City = "Wellington",
                BirthDate = new DateTime(1992, 05, 19)
            });

            _unitOfWork.Setup(uow => uow.Doctors.GetOneAsync(7, true)).ReturnsAsync(new Doctor
            {
                Id = 7,
                FirstName = "Ronaldo",
                LastName = "Fenomeno",
                ExtraDetails = "Joga Muito",
                Address = "Test",
                City = "Wellington",
                RegistrationNumber = 9999
            });

            //Act
            var result = _bookings.SaveBookings(appointmentDTO);

            //Assert
            Assert.Equal(earlyMorningAppointmentPrice, result.Price);

            _unitOfWork.Verify(x => x.Appointments.CreateAsync(It.IsAny<Appointment>()), Times.Once());
        }
    }
}
