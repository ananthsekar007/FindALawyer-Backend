using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FindALawyer.Data;
using FindALawyer.Models;
using FindALawyer.Services.PaymentService;
using FindALawyer.Dao;
using FindALawyer.Dao.PaymentDao;
using Razorpay.Api;

namespace FindALawyer.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;
        public PaymentsController(IPaymentService paymentService, IConfiguration configuration) { 
            _paymentService = paymentService;
            _configuration = configuration;
        }

        [HttpPost("request")]
        public async Task<ActionResult<ServiceResponse<string>>> RequestPayment(RequestPayment requestPayment)
        {
            ServiceResponse<string> response = await _paymentService.RequestPayment(requestPayment.AppointmentId, requestPayment.Amount);
            if(response.Error is not null) return BadRequest(response.Error);
            return Ok(response.Response);
        }

        [HttpPost("create-order")]
        public async Task<ActionResult<ServiceResponse<RazorPayPayments>>> CreateOrder(CreateOrder createOrder)
        {
            ServiceResponse<RazorPayPayments> response = await _paymentService.CreateOrder(createOrder.PaymentId, createOrder.Amount, _configuration);
            if (response.Error is not null) return BadRequest(response.Error);
            return Ok(response.Response);
        }

        [HttpPost("success")]
        public async Task<ActionResult<ServiceResponse<string>>> UpdatePaymentOnSuccess(UpdatePayment updatePayment)
        {
            ServiceResponse<string> response = await _paymentService.UpdatePaymentOnSuccess(updatePayment.OrderId, updatePayment.PaymentId);
            if (response.Error is not null) return BadRequest(response.Error);
            return Ok(response.Response);
        }
    }
}
