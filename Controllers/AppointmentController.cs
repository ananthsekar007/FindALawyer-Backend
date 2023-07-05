using System;
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
        public async Task<ActionResult<ServiceResponse<string>>> BookAppointment(AddAppointmentInput addAppointmentInput)
        {
            ServiceResponse<string> addAppointment= await _appointmentService.BookAppointment(addAppointmentInput);

            if(addAppointment.Error is not null) return BadRequest(addAppointment.Error);
            return Ok(addAppointment.Response);
        }

        [HttpGet("client/get")]
        public async Task<ActionResult<ServiceResponse<ICollection<Appointment>>>> GetAppointmentsForClients([FromQuery] int clientId, [FromQuery] string status)
        {
            ServiceResponse<ICollection<Appointment>> response = await _appointmentService.GetAppointmentsForClients(clientId, status);
            return Ok(response);
        }

        [HttpGet("lawyers/get")]
        public async Task<ActionResult<ServiceResponse<ICollection<Appointment>>>> GetAppointmentsForLawyers([FromQuery] int lawyerId, [FromQuery] string status)
        {
            ServiceResponse<ICollection<Appointment>> response = await _appointmentService.GetAppointmentsForLawyers(lawyerId, status);
            return Ok(response);
        }
    }
}
