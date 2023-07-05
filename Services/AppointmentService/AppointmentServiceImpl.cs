using FindALawyer.Dao;
using FindALawyer.Dao.AppointmentDao;
using FindALawyer.Data;
using FindALawyer.Models;
using FindALawyer.Services.ClientService;
using FindALawyer.Services.LawyerService;

namespace FindALawyer.Services.AppointmentService
{
    public class AppointmentServiceImpl : IAppointmentService
    {

        private readonly FindALawyerContext _context;
        private readonly IClientService _clientService;
        private readonly ILawyerService _lawyerService;

        public AppointmentServiceImpl(FindALawyerContext context, IClientService clientService, ILawyerService lawyerService) {
            _context = context;
            _clientService = clientService;
            _lawyerService = lawyerService;
        }

        public async Task<ServiceResponse<string>> BookAppointment(AddAppointmentInput addAppointmentInput)
        {
            ServiceResponse<string> appointmentResponse = new ServiceResponse<string>();
            bool isClientExists = await _clientService.IsValidClient(addAppointmentInput.ClientId);
            if(!isClientExists)
            {
                appointmentResponse.Error = "Not a valid Client!";
                return appointmentResponse;
            }

            bool IsLawyerExists = await _lawyerService.IsValidLawyer(addAppointmentInput.LawyerId);

            if(!IsLawyerExists)
            {
                appointmentResponse.Error = "Not a valid Lawyer!";
                return appointmentResponse;
            }

            Appointment newAppointment = new Appointment()
            {
                CaseDescription = addAppointmentInput.CaseDescription,
                LawyerId = addAppointmentInput.LawyerId,
                ClientId = addAppointmentInput.ClientId,
                Status = "PENDING"
            };

            await _context.Appointment.AddAsync(newAppointment);
            await _context.SaveChangesAsync();

            appointmentResponse.Response = "Appointment booked successfully!";
            return appointmentResponse;

        }
    }
}
