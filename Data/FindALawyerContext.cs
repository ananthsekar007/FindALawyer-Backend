using Microsoft.EntityFrameworkCore;

namespace FindALawyer.Data
{
    public class FindALawyerContext: DbContext
    {
        public FindALawyerContext(DbContextOptions<FindALawyerContext> options)
    : base(options)
        {
        }
    }
}
