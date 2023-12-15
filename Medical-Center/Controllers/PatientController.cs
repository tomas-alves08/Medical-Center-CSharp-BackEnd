using AutoMapper;
using DocuSign.eSign.Model;
using Medical_Center_Business.Business;
using Medical_Center_Common.Models.DTO.EmailData;
using Medical_Center_Common.Models.DTO.PatientData;
using Medical_Center_Data.Data.Models;
using Medical_Center_Data.Data.Repository.IRepository;
using Medical_Center_Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Center.Controllers
{
    [Route("api/Patients")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientBusiness _patientRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _dbUnitOfWork;
        private readonly IEmailService _emailService;

        public PatientController(IUnitOfWork dbUnitOfWork, IMapper mapper, IPatientBusiness patientRepo, IEmailService emailService)
        {
            _dbUnitOfWork = dbUnitOfWork;
            _mapper = mapper;
            _patientRepo = patientRepo;
            _emailService = emailService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> getAllPatients()
        {
            try
            {
                var patients = await _patientRepo.GetAllPatientsAsync();
                                        
                return Ok(_mapper.Map<IEnumerable<PatientDTO>>(patients));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We had problems when retrieving the patients' profiles.");
            }
        }

        [HttpGet("id")]
        [Authorize]
        public async Task<ActionResult<PatientDTO>> getOnePatient(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("Invalid ID. Please provide a valid ID.");
                }

                var patient = await _patientRepo.GetOnePatientAsync(id);

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
        [Authorize(Roles = "Admin")]
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
                await _patientRepo.CreatePatientAsync(model);

                createDTO.Id = model.Id;

                EmailDTO request = new EmailDTO()
                {
                    To = "ta3117362@gmail.com",
                    Subject = "Patient Account Created - Medical Services",
                    Body = $@"Hello Mr./Mrs. {model.FirstName} {model.LastName},

                        Your user was succesfully created. 


                        Sincerely,
                        
                        Medical Center"
                };

                _emailService.SendEmail(request);

                return CreatedAtAction(nameof(CreatePatient), new { id = model.Id }, createDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We had problems when creating the new patient's profile.");
            }
        }

        [HttpDelete("id")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RemovePatient(int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest("Invalid ID. Please provida a valid ID.");
                }

                var patient = await _patientRepo.GetOnePatientAsync(id);
            
                if(patient == null)
                {
                    return NotFound();
                }

                await _patientRepo.RemovePatientAsync(patient);

                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We had problems when deleting the patient's profile.");
            }
        }

        [HttpPut("id")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdatePatient(int id, [FromBody] UpdatePatientDTO updateDTO)
        {
            try
            {
                if(id == 0 || id != updateDTO.Id || updateDTO == null)
                {
                    return BadRequest("Either the provided ID is invalid or the ID provided on the update data does not match the provided ID.");
                }

                var patientToUpdate = await _patientRepo.GetOnePatientAsync(id, false);

                if(patientToUpdate == null)
                {
                    return NotFound();
                }

                var model = _mapper.Map<Patient>(updateDTO);
                await _patientRepo.UpdatePatientAsync(model);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! We had problems when updating the patient's profile.");
            }
        }
    }
}
