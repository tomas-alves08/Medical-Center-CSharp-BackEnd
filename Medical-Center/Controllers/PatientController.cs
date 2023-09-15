using AutoMapper;
using Medical_Center.Data;
using Medical_Center.Data.Models;
using Medical_Center_Common.Models.DTO.PatientData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medical_Center.Controllers
{
    [Route("api/Patients")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PatientController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDTO>>> getAllPatients()
        {
            var patients = await _db.Patients
                                    .Include(patient => patient.Appointments)
                                    .OrderBy(patient => patient.FirstName)
                                    .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<PatientDTO>>(patients));
        }

        [HttpGet("id")]
        public async Task<ActionResult<PatientDTO>> getOnePatient(int id)
        {
            if(id == 0)
            {
                return BadRequest("Invalid ID. Please provide a valid ID.");
            }

            var patient = await _db.Patients
                                   .Include (patient => patient.Appointments)
                                   .FirstOrDefaultAsync(patient => patient.Id == id);

            if(patient == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<PatientDTO>(patient);

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<PatientDTO>> CreatePatient([FromBody] CreatePatientDTO createDTO)
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
            await _db.Patients.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(CreatePatient), new { id = model.Id }, createDTO);
        }

        [HttpDelete("id")]
        public async Task<ActionResult> RemovePatient(int id)
        {
            if(id == 0)
            {
                return BadRequest("Invalid ID. Please provida a valid ID.");
            }

            var patient = await _db.Patients.FirstOrDefaultAsync(patient => patient.Id == id);
            
            if(patient == null)
            {
                return NotFound();
            }

            _db.Patients.Remove(patient);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdatePatient(int id, [FromBody] UpdatePatientDTO updateDTO)
        {
            if(id == 0 || id != updateDTO.Id || updateDTO == null)
            {
                return BadRequest("Either the provided ID is invalid or the ID provided on the update data does not match the provided ID.");
            }

            var patientToUpdate = await _db.Patients.AsNoTracking().FirstOrDefaultAsync(patient => patient.Id == id);

            if(patientToUpdate == null)
            {
                return NotFound();
            }

            Patient model = _mapper.Map<Patient>(updateDTO);
            _db.Patients.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
