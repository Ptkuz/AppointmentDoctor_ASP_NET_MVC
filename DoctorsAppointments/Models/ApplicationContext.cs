using Microsoft.EntityFrameworkCore;
using DoctorsAppointments.Models.DataBase;


namespace DoctorsAppointments.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Profile> Profiles { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        { 
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder) 
        {            
            builder.Entity<Appointment>()
            .Property(c => c.DateAppointment)
            .HasColumnType("date");

            builder.Entity<Doctor>()
            .Property(c => c.DateAge)
            .HasColumnType("date");

            builder.Entity<Patient>()
            .Property(c => c.DateAge)
            .HasColumnType("date");


        }

    }
}
