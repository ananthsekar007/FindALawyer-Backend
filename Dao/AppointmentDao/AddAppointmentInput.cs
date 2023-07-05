namespace FindALawyer.Dao.AppointmentDao
{
    public class AddAppointmentInput
    {
        public int ClientId { get; set; }
        
        public int LawyerId { get; set; }

        public string CaseDescription { get; set; } = string.Empty;
    }
}
