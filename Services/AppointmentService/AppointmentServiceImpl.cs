using FindALawyer.Dao;
using FindALawyer.Dao.AppointmentDao;
using FindALawyer.Data;
using FindALawyer.Models;
using FindALawyer.Services.ClientService;
using FindALawyer.Services.LawyerService;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ServiceResponse<ICollection<Appointment>>> GetAppointmentsForClients(int clientId, string status)
        {
            ServiceResponse<ICollection<Appointment>> response = new ServiceResponse<ICollection<Appointment>>();

            ICollection<Appointment> appointments = await _context.Appointment.Include(a => a.Client).Include(a => a.Lawyer).Where(a => a.ClientId == clientId && a.Status == status).ToListAsync();

            response.Response = appointments;
            return response;
        }

        public async Task<ServiceResponse<ICollection<Appointment>>> GetAppointmentsForLawyers(int clientId, string status)
        {
            ServiceResponse<ICollection<Appointment>> response = new ServiceResponse<ICollection<Appointment>>();

            ICollection<Appointment> appointments = await _context.Appointment.Include(a => a.Client).Include(a =>a.Lawyer).Where(a => a.LawyerId == clientId && a.Status == status).ToListAsync();

            response.Response = appointments;
            return response;
        }

        public async Task<ServiceResponse<string>> UpdateStatus(int appointmentId, string status)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();

            Appointment existingAppointment = await _context.Appointment.FindAsync(appointmentId);

            if(existingAppointment is null)
            {
                response.Error = "No appointment available with the given details!";
                return response;
            }

            existingAppointment.Status = status;
            await _context.SaveChangesAsync();

            response.Response = "Appointment Updated successfully!";
            return response;

        }
    }
}
