using AutoMapper;
using Medical_Center.Data;
using Medical_Center.Data.Models;
using Medical_Center.Data.Repository.IRepository;
using Medical_Center_Common.Models.DTO.AppointmentData;
using Medical_Center_Common.Models.DTO.DoctorData;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medical_Center.Controllers
{
    [Route("api/Appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Appointment> _dbAppointment;
        private readonly IRepository<Doctor> _dbDoctor;
        private readonly IRepository<Patient> _dbPatient;
        private readonly ApplicationDbContext _db;
        public AppointmentController(ApplicationDbContext db,IRepository<Appointment> dbAppointment, IMapper mapper, IRepository<Doctor> dbDoctor, IRepository<Patient> dbPatient)
        {
            _dbAppointment = dbAppointment;
            _mapper = mapper;
            _db = db;
            _dbDoctor = dbDoctor;
            _dbPatient = dbPatient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AppointmentDTO>> GetAllAppointments()
        {
            try
            {
                IEnumerable<Appointment> appointments = _dbAppointment.GetAll();

                return Ok(_mapper.Map<IEnumerable<AppointmentDTO>>(appointments));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "I am sorry but we had a problem when trying to retreive your appointment information.");
            }
        }

        [HttpGet("id")]
        public ActionResult<AppointmentDTO> GetOneAppointment(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest();
                }

                var appointment = _dbAppointment.GetOne(appointment => appointment.Id == id);

                if (appointment == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<AppointmentDTO>(appointment));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "I am sorry but we had a problem when trying to retreive your appointment information.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDTO>> CreateAppointment(CreateAppointmentDTO createDTO)
        {
            try
            {
                /*var doctor = _db.Doctors.FirstOrDefault(doc => doc.Id == createDTO.DoctorId);
                var patient = _db.Patients.FirstOrDefault(patient => patient.Id == createDTO.PatientId);*/

                var doctor = _dbDoctor.GetOne(doc => doc.Id == createDTO.DoctorId);
                var patient = _dbPatient.GetOne(patient => patient.Id == createDTO.PatientId);

                if (doctor == null)
                {
                    return BadRequest("Doctor does not exist. Please, try selecting a different one.");
                }

                if (patient == null)
                {
                    return BadRequest("Patient does not exist. Please, try selecting a different one.");
                }

                if (createDTO == null)
                {
                    return BadRequest("Invalid Appointment Data.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Appointment model = _mapper.Map<Appointment>(createDTO);
                await _dbAppointment.CreateAsync(model);

                return CreatedAtAction(nameof(CreateAppointment), new {id = model.Id}, createDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "I am sorry but we had a problem when trying to create your appointment.");
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult> RemoveAppointment(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid ID. Please try using a different ID.");
                }

                var appointment = _dbAppointment.GetOne(appointment => appointment.Id == id);

                if (appointment == null)
                {
                    return NotFound();
                }

                await _dbAppointment.RemoveAsync(appointment);

                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry but we had problems when trying to delete your appointment.");
            }
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentDTO updateDTO) 
        {
            /*var patient = _db.Patients.FirstOrDefault(patient => patient.Id == updateDTO.PatientId);
            var doctor = _db.Doctors.FirstOrDefault(doc => doc.Id == updateDTO.DoctorId);*/

            var doctor = _dbDoctor.GetOne(doc => doc.Id == updateDTO.DoctorId);
            var patient = _dbPatient.GetOne(patient => patient.Id == updateDTO.PatientId);

            if (patient == null || doctor == null)
            {
                return BadRequest("At least Patient or Doctor chosen does not exist. Please double check your appointment request.");
            }

            if(id == 0 || updateDTO.Id != id || updateDTO == null)
            {
                return BadRequest("Either id is not valid or id provided and id of the appointment provided do not match.");
            }

            var appointment = await _db.Appointments.AsNoTracking().FirstOrDefaultAsync(app => app.Id == id);

            if(appointment == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<Appointment>(updateDTO);

            await _dbAppointment.UpdateAsync(model);

            return NoContent();
        }
    }
}
