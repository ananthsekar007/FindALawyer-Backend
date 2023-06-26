using FindALawyer.Models;
using Microsoft.EntityFrameworkCore;

namespace FindALawyer.Data
{
    public class FindALawyerContext: DbContext
    {
        public FindALawyerContext(DbContextOptions<FindALawyerContext> options)
    : base(options)
        {
        }

        public DbSet<Client> Client { get; set; } = default!;
    }
}
