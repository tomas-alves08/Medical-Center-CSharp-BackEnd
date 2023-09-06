using Medical_Center.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Medical_Center.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                    PatientId = 1,
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
                    DoctorId = 1,
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
    }
}
