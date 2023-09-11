using Microsoft.EntityFrameworkCore;

namespace CW8.Models
{
    public class Cw8DBContext : DbContext
    {

        public Cw8DBContext(DbContextOptions options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Doctor>(e =>
            {
                e.HasData(new List<Doctor>
                {
                    new Doctor
                    {
                        IdDoctor = 1,
                        FirstName = "Aleksander",
                        LastName = "Babij",
                        Email = "123@gmail.com"
                    },
                    new Doctor
                    {
                        IdDoctor = 2,
                        FirstName = "Jan",
                        LastName = "Kowalski",
                        Email = "456@gmail.com"
                    },
                    new Doctor
                    {
                        IdDoctor = 3,
                        FirstName = "Ignacy",
                        LastName = "Zalewski",
                        Email = "abc@gmail.com"
                    }
                });
            });

            modelBuilder.Entity<Patient>(e =>
            {
                e.HasData(new List<Patient>
                {
                    new Patient
                    {
                        IdPatient = 1,
                        FirstName = "Zbysław",
                        LastName = "Nowak",
                        Birthdate = DateTime.Parse("01/01/2000"),
                    },
                    new Patient
                    {
                        IdPatient = 2,
                        FirstName = "Przemysław",
                        LastName = "Zły",
                        Birthdate = DateTime.Parse("05/05/1998"),
                    },
                    new Patient
                    {
                        IdPatient = 3,
                        FirstName = "Kacper",
                        LastName = "Dobry",
                        Birthdate = DateTime.Parse("16/10/1990"),
                    },
                });
            });

            modelBuilder.Entity<Prescription>(e =>
            {

                e.HasData(new List<Prescription>
                {
                    new Prescription
                    {
                        IdPrescription = 1,
                        Date = DateTime.UtcNow,
                        DueDate = DateTime.UtcNow,
                        IdDoctor = 1,
                        IdPatient = 1
                    },
                    new Prescription
                    {
                        IdPrescription = 2,
                        Date = DateTime.UtcNow,
                        DueDate = DateTime.UtcNow,
                        IdDoctor = 3,
                        IdPatient = 2

                    }
                });

            });

            modelBuilder.Entity<Medicament>(e =>
            {
                e.HasData(new List<Medicament>
                {
                    new Medicament
                    {
                        IdMedicament = 1,
                        Name = "Ibuprofen",
                        Description = "lek na głowe",
                        Type = "przeciwbólowy"
                    },
                    new Medicament
                    {
                        IdMedicament = 2,
                        Name = "Syrop",
                        Description = "na kaszel",
                        Type = "na ból gardła i kaszel"
                    }
                });
            });

            modelBuilder.Entity<Prescription_Medicament>(e =>
            {
                e.HasData(new List<Prescription_Medicament>
                {
                    new Prescription_Medicament
                    {
                        IdPrescription = 1,
                        IdMedicament = 1,
                        Dose = 5,
                        Details = "5x dziennie"
                    },
                    new Prescription_Medicament
                    {
                        IdPrescription = 2,
                        IdMedicament = 2,
                        Dose = null,
                        Details = "1x dziennie"
                    },
                });
            });

           

        }

    }
}
