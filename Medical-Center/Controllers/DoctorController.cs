using AutoMapper;
using Medical_Center_Business.Business;
using Medical_Center_Common.Models.DTO.DoctorData;
using Medical_Center_Data.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Center.Controllers
{
    [Route("api/Doctors")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDoctorBusiness _doctorRepo;

        public DoctorController(IMapper mapper, IDoctorBusiness doctorRepo)
        {
            _mapper = mapper;
            _doctorRepo = doctorRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorDTO>>> GetAllDoctors()
        {
            try
            {
                var doctors = await _doctorRepo.GetAlldoctorsAsync();

                return Ok(_mapper.Map<IEnumerable<DoctorDTO>>(doctors));
            }
            catch (Exception)
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

                var doctor = await _doctorRepo.GetOnedoctorAsync(id);

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

                var doctorExistingRegistration = await _doctorRepo.GetOnedoctorAsync(createDTO.RegistrationNumber); 

                if(doctorExistingRegistration != null)
                {
                    return BadRequest("Doctor already registered.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Doctor model = _mapper.Map<Doctor>(createDTO);
                await _doctorRepo.CreateDoctorAsync(model);

                createDTO.Id = model.Id;

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

                var doctorToDelete = await _doctorRepo.GetOnedoctorAsync(id);

                if(doctorToDelete == null)
                {
                    return NotFound();
                }

                await _doctorRepo.RemoveDoctorAsync(doctorToDelete);

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

                var doctorToUpdate = await _doctorRepo.GetOnedoctorAsync(id, false);

                if(doctorToUpdate == null)
                {
                    return NotFound();
                }

                var model = _mapper.Map<Doctor>(updateDTO);
                await _doctorRepo.UpdateDoctorAsync(model);

                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We are having problems to save your doctor's info.");
            }
        }
    }
}
