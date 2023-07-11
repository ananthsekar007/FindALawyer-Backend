using FindALawyer.Dao;
using FindALawyer.Models;

namespace FindALawyer.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<ServiceResponse<RazorPayPayments>> CreateOrder(int paymentId, float amount, IConfiguration configuration);

        Task<ServiceResponse<string>> RequestPayment(int appointmentId, float amount);

        Task<ServiceResponse<string>> UpdatePaymentOnSuccess(string orderId,  string paymentId);

        Task<ServiceResponse<ICollection<Payment>>> GetAllPaymentsForAppointment(int appointmentId);
    }
}
