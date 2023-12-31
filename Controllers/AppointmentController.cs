﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindALawyer.Data;
using FindALawyer.Models;
using FindALawyer.Dao;
using FindALawyer.Dao.AppointmentDao;
using FindALawyer.Services.AppointmentService;
using System.Security.Policy;
using Microsoft.AspNetCore.Authorization;

namespace FindALawyer.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService) {
            _appointmentService = appointmentService;
        }

        [HttpPost("book")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<string>>> BookAppointment(AddAppointmentInput addAppointmentInput)
        {
            ServiceResponse<string> addAppointment= await _appointmentService.BookAppointment(addAppointmentInput);

            if(addAppointment.Error is not null) return BadRequest(addAppointment.Error);
            return Ok(addAppointment.Response);
        }

        [HttpGet("client/get")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<ICollection<Appointment>>>> GetAppointmentsForClients([FromQuery] int clientId, [FromQuery] string status)
        {
            ServiceResponse<ICollection<Appointment>> response = await _appointmentService.GetAppointmentsForClients(clientId, status);
            return Ok(response);
        }

        [HttpGet("lawyers/get")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<ICollection<Appointment>>>> GetAppointmentsForLawyers([FromQuery] int lawyerId, [FromQuery] string status)
        {
            ServiceResponse<ICollection<Appointment>> response = await _appointmentService.GetAppointmentsForLawyers(lawyerId, status);
            return Ok(response);
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<string>>> UpdateAppointment(UpdateAppointmentInput updateAppointmentInput)
        {
            ServiceResponse<string> updateResponse = await _appointmentService.UpdateStatus(updateAppointmentInput.AppointmentId, updateAppointmentInput.Status);

            if (updateResponse.Error is not null) return BadRequest(updateResponse.Error);
            return Ok(updateResponse);
        }

        [HttpPut("complete")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<string>>> CompleteAppointment(UpdateAppointmentInput input)
        {
            ServiceResponse<string> updateResponse = await _appointmentService.CompleteAppointment(input.AppointmentId);

            if (updateResponse.Error is not null) return BadRequest(updateResponse.Error);
            return Ok(updateResponse);
        }
    }
}
