using AutoMapper;
using Medical_Center.Data;
using Medical_Center.Models;
using Medical_Center.Models.DTO.AppointmentDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace Medical_Center.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public AppointmentController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAppointments()
        {
            try
            {
                IEnumerable<Appointment> appointments = await _db.Appointments
                                                            .Include(appointment => appointment.Patient)
                                                            .Include(appointment => appointment.Doctor)
                                                            .OrderBy(appointment => appointment.AppointmentDateTime)
                                                            .ToListAsync();

                return Ok(_mapper.Map<IEnumerable<AppointmentDTO>>(appointments));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "I am sorry but we had a problem when trying to retreive your appointment information.");
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<AppointmentDTO>> GetOneAppointment(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest();
                }

                Appointment appointment = await _db.Appointments
                                                    .Include(appointment => appointment.Patient)
                                                    .Include(appointment => appointment.Doctor)
                                                    .FirstOrDefaultAsync(appointment => appointment.Id == id);

                if (appointment == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<AppointmentDTO>(appointment));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "I am sorry but we had a problem when trying to retreive your appointment information.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<AppointmentDTO>> CreateAppointment(CreateAppointmentDTO createDTO)
        {
            try
            {
                var doctor = _db.Doctors.FirstOrDefault(doc => doc.DoctorId == createDTO.DoctorId);
                var patient = _db.Patients.FirstOrDefault(patient => patient.PatientId == createDTO.PatientId);

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
                await _db.Appointments.AddAsync(model);
                await _db.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateAppointment), new {id = model.Id}, createDTO);
            }
            catch (Exception ex)
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

                var appointment = _db.Appointments.FirstOrDefault(appointment => appointment.Id == id);

                if (appointment == null)
                {
                    return NotFound();
                }

                _db.Remove(appointment);
                await _db.SaveChangesAsync();

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry but we had problems when trying to delete your appointment.");
            }
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentDTO updateDTO) 
        {
            var patient = _db.Patients.FirstOrDefault(patient => patient.PatientId == updateDTO.PatientId);
            var doctor = _db.Doctors.FirstOrDefault(doc => doc.DoctorId == updateDTO.DoctorId);
            
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
            _db.Update(model);

            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
