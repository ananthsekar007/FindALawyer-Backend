namespace FindALawyer.Dao.AppointmentDao
{
    public class UpdateAppointmentInput
    {
        public int AppointmentId { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
