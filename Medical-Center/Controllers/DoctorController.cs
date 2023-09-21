using AutoMapper;
using Medical_Center.Data.Models;
using Medical_Center.Data.Repository.IRepository;
using Medical_Center_Common.Models.DTO.DoctorData;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Center.Controllers
{
    [Route("api/Doctors")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IRepository<Doctor> _dbDoctor;
        private readonly IMapper _mapper;

        public DoctorController(IRepository<Doctor> dbDoctor, IMapper mapper)
        {
            _dbDoctor = dbDoctor;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DoctorDTO>> GetAllDoctors()
        {
            try
            {
                var doctors = _dbDoctor.GetAll();

                return Ok(_mapper.Map<IEnumerable<DoctorDTO>>(doctors));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry but we are having problems when retreiving your list of doctors.");
            }
        }

        [HttpGet("id")]
        public ActionResult<DoctorDTO> GetOneDoctor(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("ID provided is not valid. Please try using a different one.");
                }

                var doctor = _dbDoctor.GetOne(doc => doc.Id == id);

                if(doctor == null)
                {
                    return NotFound();
                }

                var model = _mapper.Map<DoctorDTO>(doctor);

                return Ok(model);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry but we are having problems when trying to retrieve the specified doctor info.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<DoctorDTO>> CreateDoctor([FromBody] CreateDoctorDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest("Invalid Doctor Information.");
                }

                var doctorExistingRegistration = _dbDoctor.GetOne(doc => doc.RegistrationNumber == createDTO.RegistrationNumber); 

                if(doctorExistingRegistration != null)
                {
                    return BadRequest("Doctor already registered.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Doctor model = _mapper.Map<Doctor>(createDTO);
                await _dbDoctor.CreateAsync(model);

                return CreatedAtAction(nameof(CreateDoctor), new { id = model.Id }, createDTO);
            }
            catch (Exception)
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

                var doctorToDelete = _dbDoctor.GetOne(doc => doc.Id == id);

                if(doctorToDelete == null)
                {
                    return NotFound();
                }

                await _dbDoctor.RemoveAsync(doctorToDelete);

                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We are having problems when trying to delete your doctor user.");
            }
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateDoctor(int id, [FromBody] UpdateDoctorDTO updateDTO)
        {
            try
            {
                if(id == 0 || id != updateDTO.Id || updateDTO == null)
                {
                    return BadRequest("Please double check the ID and doctor info ID.");
                }

                var doctorToUpdate = _dbDoctor.GetOne(doc => doc.Id == id, false);

                if(doctorToUpdate == null)
                {
                    return NotFound();
                }

                var model = _mapper.Map<Doctor>(updateDTO);
                await _dbDoctor.UpdateAsync(model);

                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We are having problems to save your doctor's info.");
            }
        }
    }
}
