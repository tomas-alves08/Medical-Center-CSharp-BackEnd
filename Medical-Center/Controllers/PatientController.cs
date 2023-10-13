using AutoMapper;
using Medical_Center.Data.Models;
using Medical_Center.Data.Repository.IRepository;
using Medical_Center_Common.Models.DTO.PatientData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medical_Center.Controllers
{
    [Route("api/Patients")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IRepo<Patient> _dbPatient;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _dbUnitOfWork;

        public PatientController(IUnitOfWork dbUnitOfWork, IMapper mapper)
        {
            _dbUnitOfWork = dbUnitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> getAllPatients()
        {
            try
            {
                var patients = await _dbUnitOfWork.Patients.GetAllAsync();
                                        
                return Ok(_mapper.Map<IEnumerable<PatientDTO>>(patients));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We had problems when retrieving the patients' profiles.");
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<PatientDTO>> getOnePatient(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("Invalid ID. Please provide a valid ID.");
                }

                var patient = await _dbUnitOfWork.Patients.GetOneAsync(id);

                if(patient == null)
                {
                    return NotFound();
                }

                var model = _mapper.Map<PatientDTO>(patient);

                return Ok(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We had problems when retrieving the patient's profile.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PatientDTO>> CreatePatient([FromBody] CreatePatientDTO createDTO)
        {
            try
            {
                if (createDTO == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Patient model = _mapper.Map<Patient>(createDTO);
                await _dbUnitOfWork.Patients.CreateAsync(model);
                await _dbUnitOfWork.Save();

                createDTO.Id = model.Id;

                return CreatedAtAction(nameof(CreatePatient), new { id = model.Id }, createDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We had problems when creating the new patient's profile.");
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult> RemovePatient(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("Invalid ID. Please provida a valid ID.");
                }

                var patient = await _dbUnitOfWork.Patients.GetOneAsync(id);
            
                if(patient == null)
                {
                    return NotFound();
                }

                await _dbUnitOfWork.Patients.RemoveAsync(patient);
                await _dbUnitOfWork.Save();

                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We had problems when deleting the patient's profile.");
            }
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdatePatient(int id, [FromBody] UpdatePatientDTO updateDTO)
        {
            try
            {
                if(id == 0 || id != updateDTO.Id || updateDTO == null)
                {
                    return BadRequest("Either the provided ID is invalid or the ID provided on the update data does not match the provided ID.");
                }

                var patientToUpdate = await _dbUnitOfWork.Patients.GetOneAsync(id, false);

                if(patientToUpdate == null)
                {
                    return NotFound();
                }

                var model = _mapper.Map<Patient>(updateDTO);
                await _dbUnitOfWork.Patients.UpdateAsync(model);
                await _dbUnitOfWork.Save();

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We had problems when updating the patient's profile.");
            }
        }
    }
}
