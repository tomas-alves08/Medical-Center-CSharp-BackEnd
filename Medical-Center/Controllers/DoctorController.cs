using AutoMapper;
using Medical_Center.Data;
using Medical_Center.Models;
using Medical_Center.Models.DTO.AppointmentDTO;
using Medical_Center.Models.DTO.DoctorDTO;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Medical_Center.Controllers
{
    [Route("api/Doctors")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public DoctorController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetAllDoctors()
        {
            try
            {
                IEnumerable<Doctor> doctors = await _db.Doctors
                                                            .Include(doc => doc.Appointments)
                                                            .OrderBy(doc => doc.FirstName)
                                                            .ToListAsync();

                return Ok(_mapper.Map<IEnumerable<DoctorDTO>>(doctors));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry but we are having problems when retreiving your list of doctors.");
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<DoctorDTO>> GetOneDoctor(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("ID provided is not valid. Please try using a different one.");
                }

                var doctor = _db.Doctors.Include(doc => doc.Appointments).FirstOrDefault(doc => doc.DoctorId == id);

                if(doctor == null)
                {
                    return NotFound();
                }

                var model = _mapper.Map<DoctorDTO>(doctor);

                return Ok(model);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry but we are having problems when trying to retrieve the specified doctor info.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDTO>> CreateDoctor([FromBody] DoctorDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest("Invalid Doctor Information.");
                }

                var doctorExistingRegistration = _db.Doctors.FirstOrDefault(doc => doc.RegistrationNumber == createDTO.RegistrationNumber); 

                if(doctorExistingRegistration != null)
                {
                    return BadRequest("Doctor already registered.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Doctor model = _mapper.Map<Doctor>(createDTO);
                await _db.Doctors.AddAsync(model);
                await _db.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateDoctor), new { id = model.DoctorId }, createDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry but we are having problems when trying to create a doctor user.");
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult> RemoveDoctor(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("ID invalid");
                }

                var doctorToDelete = _db.Doctors.FirstOrDefault(doc => doc.DoctorId == id);

                if(doctorToDelete == null)
                {
                    return NotFound();
                }

                _db.Remove(doctorToDelete);
                await _db.SaveChangesAsync();

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We are having problems when trying to delete your doctor user.");
            }
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateDoctor(int id, [FromBody] DoctorDTO updateDTO)
        {
            try
            {
                if(id == 0 || id != updateDTO.DoctorId)
                {
                    return BadRequest("Please double check the ID and doctor info ID.");
                }

                var doctorToUpdate = _db.Doctors.FirstOrDefault(doc => doc.DoctorId == id);

                if(doctorToUpdate == null)
                {
                    return NotFound();
                }

                var model = _mapper.Map<Doctor>(updateDTO);
                _db.Update(model);
                await _db.SaveChangesAsync();

                return NoContent();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We are having problems to save your doctor's info.");
            }
        }
    }
}
