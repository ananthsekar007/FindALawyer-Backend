using FindALawyer.Dao;
using FindALawyer.Data;
using FindALawyer.Models;
using FindALawyer.Services.AppointmentService;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Razorpay.Api;

namespace FindALawyer.Services.PaymentService
{
    public class PaymentServiceImpl : IPaymentService
    {
        private readonly IAppointmentService _appointmentService;
        private readonly FindALawyerContext _context;

        public PaymentServiceImpl(FindALawyerContext context, IAppointmentService appointmentService) {
            _appointmentService = appointmentService;
            _context = context;
        }

        public async Task<ServiceResponse<RazorPayPayments>> CreateOrder(int paymentId, float amount, IConfiguration configuration)
        {
            ServiceResponse<RazorPayPayments> response = new ServiceResponse<RazorPayPayments>();

            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", amount * 100);
            input.Add("currency", "INR");

            PaymentKey keys = new PaymentKey(configuration);

            RazorpayClient client = new RazorpayClient(keys.Key, keys.Secret);

            Order order =  client.Order.Create(input);
            if (order is null) {
                response.Error = "Order not created successfully!";
                return response;
            }
            string orderId = order["id"].ToString();

            RazorPayPayments newPayment = new RazorPayPayments()
            {
                Amount = amount,
                PaymentOrderId = orderId,
                PaymentId = null
            };

            await _context.RazorPayments.AddAsync(newPayment);
            await _context.SaveChangesAsync();

            RazorPayPayments createdOrder = await _context.RazorPayments.FirstOrDefaultAsync(p => p.PaymentOrderId == orderId);

            FindALawyer.Models.Payment existingPayment = await _context.Payment.FindAsync(paymentId);
            if (existingPayment is null) {
                response.Error = "Existing payment cannot be found!";
                return response;
            }

            existingPayment.paymentReferenceId = createdOrder.RazorPayId;
            await _context.SaveChangesAsync();
            response.Response = createdOrder;
            return response;

        }

        public async Task<ServiceResponse<string>> RequestPayment(int appointmentId, float amount)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            bool isAppointmentValid = await _appointmentService.IsAppointmentValid(appointmentId);
            if(!isAppointmentValid)
            {
                response.Error = "No appointments available with the given details!";
                return response;
            }

            FindALawyer.Models.Payment newPayment = new FindALawyer.Models.Payment()
            {
                Amount = amount,
                AppointmentId = appointmentId,
                Status = "PENDING",
                paymentReferenceId = null
            };

            await _context.Payment.AddAsync(newPayment);
            await _context.SaveChangesAsync();

            response.Response = "Payment Requested successfully!";
            return response;
            
            
        }

        public async Task<ServiceResponse<string>> UpdatePaymentOnSuccess(string orderId, string paymentId)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            RazorPayPayments existingOrder = await _context.RazorPayments.FirstOrDefaultAsync(r => r.PaymentOrderId == orderId);
            if (existingOrder is null) {
                response.Error = "No payment found!";
                return response;
            }

            existingOrder.PaymentId = paymentId;
            await _context.SaveChangesAsync();

            FindALawyer.Models.Payment existingPayment = await _context.Payment.FirstOrDefaultAsync(p => p.paymentReferenceId == existingOrder.RazorPayId);

            if (existingPayment is null)
            {
                response.Error = "No payment found!";
                return response;
            }

            existingPayment.Status = "PAID";
            await _context.SaveChangesAsync();

            response.Response = "Payment Successful!";
            return response;
        }
    }
}
