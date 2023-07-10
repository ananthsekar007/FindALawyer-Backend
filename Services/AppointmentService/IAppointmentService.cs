using FindALawyer.Dao;
using FindALawyer.Dao.AppointmentDao;
using FindALawyer.Models;

namespace FindALawyer.Services.AppointmentService
{
    public interface IAppointmentService
    {
        Task<ServiceResponse<string>> BookAppointment(AddAppointmentInput addAppointmentInput);
        Task<ServiceResponse<ICollection<Appointment>>> GetAppointmentsForClients(int clientId, string status);
        Task<ServiceResponse<ICollection<Appointment>>> GetAppointmentsForLawyers(int clientId, string status);

        Task<ServiceResponse<string>> UpdateStatus(int appointmentId,  string status);

        Task<bool> IsAppointmentValid(int appointmentId);

    }
}
