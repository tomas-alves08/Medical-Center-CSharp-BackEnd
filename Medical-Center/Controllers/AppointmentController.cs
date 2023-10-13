using AutoMapper;
using Medical_Center.Data.Models;
using Medical_Center.Data.Repository.IRepository;
using Medical_Center_Common.Models.DTO.AppointmentData;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Center.Controllers
{
    [Route("api/Appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _dbUnitOfWork;
        public AppointmentController(IUnitOfWork dbUnitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _dbUnitOfWork = dbUnitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAllAppointments()
        {
            try
            {
                IEnumerable<Appointment> appointments = await _dbUnitOfWork.Appointments.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<AppointmentDTO>>(appointments));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "I am sorry but we had a problem when trying to retreive your appointment information.");
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<AppointmentDTO>> GetOneAppointment(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var appointment = await _dbUnitOfWork.Appointments.GetOneAsync(id);

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
                var patient = _dbUnitOfWork.Patients.GetOneAsync(createDTO.PatientId);
                var doctor = _dbUnitOfWork.Doctors.GetOneAsync(createDTO.DoctorId);

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
                await _dbUnitOfWork.Appointments.CreateAsync(model);
                await _dbUnitOfWork.Save();

                createDTO.Id = model.Id;

                return CreatedAtAction(nameof(CreateAppointment), new {id = model.Id}, createDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "I am sorry but we had a problem when trying to create your appointment. Error related to " + ex);
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

                var appointment = await _dbUnitOfWork.Appointments.GetOneAsync(id, false);

                if (appointment == null)
                {
                    return NotFound();
                }

                await _dbUnitOfWork.Appointments.RemoveAsync(appointment);
                await _dbUnitOfWork.Save();

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
            var doctor = _dbUnitOfWork.Doctors.GetOneAsync(updateDTO.DoctorId, false);
            var patient = _dbUnitOfWork.Patients.GetOneAsync(updateDTO.PatientId, false);

            if (patient == null || doctor == null)
            {
                return BadRequest("At least Patient or Doctor chosen does not exist. Please double check your appointment request.");
            }

            if(id == 0 || updateDTO.Id != id || updateDTO == null)
            {
                return BadRequest("Either id is not valid or id provided and id of the appointment provided do not match.");
            }

            var appointment = await _dbUnitOfWork.Appointments.GetOneAsync(id, false);

            if (appointment == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<Appointment>(updateDTO);

            await _dbUnitOfWork.Appointments.UpdateAsync(model);
            await _dbUnitOfWork.Save();

            return NoContent();
        }
    }
}
