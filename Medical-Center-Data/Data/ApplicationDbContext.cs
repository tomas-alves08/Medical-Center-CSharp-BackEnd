using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection.Metadata;
using Medical_Center_Common.Models.DTO.AppointmentData;
using Medical_Center_Common.Models.DTO.DoctorData;
using Medical_Center_Common.Models.DTO.PatientData;
using Medical_Center_Data.Data.Models;

namespace Medical_Center_Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Payment> Bookings { get; set; }
        /*public DbSet<Payment> Payments { get; set; }*/
        public DbSet<LocalUser> LocalUsers { get; set; }
        /*
        public DbSet<AppointmentOrderResponse> AppointmentOrderResponses { get; set; }*/
        /*public DbSet<Payment> Payments { get; set; }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>().HasKey(k => k.Id);
            modelBuilder.Entity<Patient>().HasKey(k => k.Id);
            modelBuilder.Entity<Doctor>().HasKey(k => k.Id);
            modelBuilder.Entity<Payment>().HasKey(k => k.Id);
            modelBuilder.Entity<LocalUser>().HasKey(k => k.Id);

            modelBuilder.Entity<Doctor>().HasMany<Appointment>(f => f.Appointments).WithOne(a => a.Doctor).HasForeignKey(a=> a.DoctorId).IsRequired();
            modelBuilder.Entity<Patient>().HasMany<Appointment>(f => f.Appointments).WithOne(a => a.Patient).HasForeignKey(a => a.PatientId).IsRequired();
            
            modelBuilder.Entity<Appointment>().Property(p => p.AppointmentDateTime).IsRequired();

            modelBuilder.Entity<Doctor>().Property(p => p.FirstName).IsRequired();
            modelBuilder.Entity<Doctor>().Property(p => p.LastName).IsRequired();
            modelBuilder.Entity<Doctor>().Property(p => p.RegistrationNumber).IsRequired();

            modelBuilder.Entity<Patient>().Property(p => p.FirstName).IsRequired();
            modelBuilder.Entity<Patient>().Property(p => p.LastName).IsRequired();

            modelBuilder.Entity<Payment>().Property(p => p.PatientId).IsRequired();
            modelBuilder.Entity<Payment>().Property(p => p.AppointmentId).IsRequired();

            modelBuilder.Entity<LocalUser>().Property(p => p.UserName).IsRequired();
            modelBuilder.Entity<LocalUser>().Property(p => p.Password).IsRequired();

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment() 
                {
                    Id = 1,
                    AppointmentDateTime = new DateTime(2023, 10, 17, 14, 30, 0),
                    PatientId = 1,
                    DoctorId = 1,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                }
            );

            modelBuilder.Entity<Patient>().HasData(
                new Patient()
                {
                    Id = 1,
                    FirstName = "Tomas",
                    LastName = "Alves de Souza",
                    Address = "8 Fake Street",
                    City = "Wellington",
                    BirthDate = new DateTime(1989, 3, 8),
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                }
            );

            modelBuilder.Entity<Doctor>().HasData(
                new Doctor()
                {
                    Id = 1,
                    FirstName = "Joseph",
                    LastName = "Smith",
                    Address = "77 Craig Street",
                    City = "Wellington",
                    RegistrationNumber = 167948,
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now
                }
            );
        }

        internal Task AddAsync(Appointment appointment, Patient patient)
        {
            throw new NotImplementedException();
        }
    }
}
