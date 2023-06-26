namespace FindALawyer.Dao
{
    public class ServiceResponse<T>
    {
        public T Response { get; set; }

        public string Error { get; set; }
    }
}
