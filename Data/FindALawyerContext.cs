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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<Lawyer>()
        //        .HasMany(l => l.Client)
        //        .WithMany()
        //        .HasForeignKey(c => c.ClientId);

        //    modelBuilder.Entity<Feedback>()
        //        .HasOne(f => f.Lawyer)
        //        .WithMany()
        //        .HasForeignKey(l => l.LawyerId);

        //}
    }
}
