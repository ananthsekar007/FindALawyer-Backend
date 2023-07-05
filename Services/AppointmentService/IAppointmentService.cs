using FindALawyer.Dao;
using FindALawyer.Dao.AppointmentDao;

namespace FindALawyer.Services.AppointmentService
{
    public interface IAppointmentService
    {
        Task<ServiceResponse<string>> BookAppointment(AddAppointmentInput addAppointmentInput);
    }
}
