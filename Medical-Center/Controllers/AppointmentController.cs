﻿using AutoMapper;
using Medical_Center_Business.Business;
using Medical_Center_Common.Models.DTO.AppointmentData;
using Medical_Center_Common.Models.DTO.EmailData;
using Medical_Center_Data.Data.Models;
using Medical_Center_Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Medical_Center.Controllers
{
    [Route("api/Appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentBusiness _appointmentRepo;
        private readonly IPatientBusiness _patientRepo;
        private readonly IDoctorBusiness _doctorRepo;
        private readonly IEmailService _emailService;
        public AppointmentController(IMapper mapper, IAppointmentBusiness appointmentRepo, IPatientBusiness patientRepo, IDoctorBusiness doctorRepo, IEmailService emailService)
        {
            _mapper = mapper;
            _appointmentRepo = appointmentRepo;
            _patientRepo = patientRepo;
            _doctorRepo = doctorRepo;
            _emailService = emailService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AppointmentDTO>>> GetAllAppointments()
        {
            try
            {
                IEnumerable<Appointment> appointments = await _appointmentRepo.GetAllAppointmentsAsync();
                return Ok(_mapper.Map<IEnumerable<AppointmentDTO>>(appointments));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "I am sorry but we had a problem when trying to retreive your appointment information.");
            }
        }

        [HttpGet("id")]
        [Authorize]
        public async Task<ActionResult<AppointmentDTO>> GetOneAppointment(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var appointment = await _appointmentRepo.GetAppointmentByIdAsync(id);

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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AppointmentDTO>> CreateAppointment(CreateAppointmentDTO createDTO)
        {
            try
            {
                var patient = await _patientRepo.GetOnePatientAsync(createDTO.PatientId, false);
                var doctor = await _doctorRepo.GetOnedoctorAsync(createDTO.DoctorId, false);

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
                await _appointmentRepo.CreateAppointmentAsync(model);

                createDTO.Id = model.Id;

                EmailDTO email = new EmailDTO()
                {
                    To = "ta3117362@gmail.com",
                    Subject = "Appointment Booking - Medical Center",
                    Body = $@"Hello Mr./Mrs. {patient.FirstName} {patient.LastName},
                             
                            Your medical appointment was succesfully booked at {model.AppointmentDateTime} with doctor {doctor.FirstName} {doctor.LastName}.

                            Sincerely,

                            Medical Center"
                };

                _emailService.SendEmail(email);

                return CreatedAtAction(nameof(CreateAppointment), new {id = model.Id}, createDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "I am sorry but we had a problem when trying to create your appointment. Error related to " + ex);
            }
        }

        [HttpDelete("id")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RemoveAppointment(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid ID. Please try using a different ID.");
                }

                var appointment = await _appointmentRepo.GetAppointmentByIdAsync(id, false);

                if (appointment == null)
                {
                    return NotFound();
                }

                EmailDTO email = new EmailDTO()
                {
                    To = "ta3117362@gmail.com",
                    Subject = "Appointment Cancelation - Medical Center",
                    Body = $@"Hello Mr./Mrs. {appointment.Patient.FirstName} {appointment.Patient.LastName},
                             
                            Your medical appointment was succesfully canceled.

                            Sincerely,

                            Medical Center"
                };

                _emailService.SendEmail(email);

                await _appointmentRepo.RemoveAppointmentAsync(appointment);

                return NoContent();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry but we had problems when trying to delete your appointment.");
            }
        }

        [HttpPut("id")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentDTO updateDTO) 
        {
            var doctor = await _doctorRepo.GetOnedoctorAsync(updateDTO.DoctorId, false);
            var patient = await _patientRepo.GetOnePatientAsync(updateDTO.PatientId, false);

            if (patient == null || doctor == null)
            {
                return BadRequest("At least Patient or Doctor chosen does not exist. Please double check your appointment request.");
            }

            if(id == 0 || updateDTO.Id != id || updateDTO == null)
            {
                return BadRequest("Either id is not valid or id provided and id of the appointment provided do not match.");
            }

            var appointment = await _appointmentRepo.GetAppointmentByIdAsync(id, false);

            if (appointment == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<Appointment>(updateDTO);

            await _appointmentRepo.UpdateAppointmentAsync(model);

            EmailDTO email = new EmailDTO()
            {
                To = "ta3117362@gmail.com",
                Subject = "Appointment Update - Medical Center",
                Body = $@"Hello Mr./Mrs. {patient.FirstName} {patient.LastName},
                             
                            Your medical appointment was succesfully updated.

                            Sincerely,

                            Medical Center"
            };

            _emailService.SendEmail(email);

            return NoContent();
        }
    }
}
