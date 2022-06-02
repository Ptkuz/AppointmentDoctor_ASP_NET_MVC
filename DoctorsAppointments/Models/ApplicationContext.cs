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
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
            

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        { 
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
               .Entity<User>()
               .HasOne(u => u.Doctor)
               .WithOne(d => d.User)
               .OnDelete(DeleteBehavior.Restrict)
               .HasForeignKey<Doctor>(d => d.UserKey);



            builder
              .Entity<User>()
              .HasOne(u => u.Patient)
              .WithOne(p => p.User)
              .OnDelete(DeleteBehavior.Restrict)
              .HasForeignKey<Patient>(p => p.UserKey);


            builder.Entity<Appointment>()
            .Property(c => c.DateAppointment)
            .HasColumnType("date");

            builder.Entity<Doctor>()
            .Property(c => c.DateAge)
            .HasColumnType("date");

            builder.Entity<Patient>()
            .Property(c => c.DateAge)
            .HasColumnType("date");

            string adminRoleName = "admin";
            string doctorRoleName = "doctor";
            string patientRoleName = "patient";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "Admin2012!";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role doctorRole = new Role { Id = 2, Name = doctorRoleName };
            Role patientRole = new Role { Id = 3, Name = patientRoleName };



            User adminUser = new User { Id = Guid.NewGuid(), Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id};


            builder.Entity<Role>().HasData(new Role[] { adminRole, doctorRole, patientRole });
            builder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(builder);


        }

    }
}
