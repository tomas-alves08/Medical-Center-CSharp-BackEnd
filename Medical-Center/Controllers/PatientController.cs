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
        private readonly IRepository<Patient> _dbPatient;
        private readonly IMapper _mapper;

        public PatientController(IRepository<Patient> dbPatient, IMapper mapper)
        {
            _dbPatient = dbPatient;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PatientDTO>> getAllPatients()
        {
            try
            {
                var patients = _dbPatient.GetAll();
                                        
                return Ok(_mapper.Map<IEnumerable<PatientDTO>>(patients));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We had problems when retrieving the patients' profiles.");
            }
        }

        [HttpGet("id")]
        public ActionResult<PatientDTO> getOnePatient(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("Invalid ID. Please provide a valid ID.");
                }

                var patient = _dbPatient.GetOne(patient => patient.Id == id);

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
                await _dbPatient.CreateAsync(model);

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

                var patient = _dbPatient.GetOne(patient => patient.Id == id);
            
                if(patient == null)
                {
                    return NotFound();
                }

                await _dbPatient.RemoveAsync(patient);

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

                var patientToUpdate = _dbPatient.GetOne(patient => patient.Id == id, false);

                if(patientToUpdate == null)
                {
                    return NotFound();
                }

                var model = _mapper.Map<Patient>(updateDTO);
                await _dbPatient.UpdateAsync(model);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We had problems when updating the patient's profile.");
            }
        }
    }
}
