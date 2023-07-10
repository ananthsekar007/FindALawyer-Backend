using FindALawyer.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FindALawyer.Data
{
    public class FindALawyerContext: DbContext
    {
        public FindALawyerContext(DbContextOptions<FindALawyerContext> options)
    : base(options)
        {
        }

        public DbSet<Client> Client { get; set; } = default!;

        public DbSet<Lawyer> Lawyer { get; set; } = default!;

        public DbSet<Feedback> Feedback { get; set; } = default!;

        public DbSet<Appointment> Appointment { get; set; } = default!;

        public DbSet<RazorPayPayments> RazorPayments { get; set; } = default!;

        public DbSet<Payment> Payment { get; set; } = default!;

    }
}
