using APBD_Z_CW4_s28038.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Z_CW4_s28038.Data;

public class MyDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    public MyDbContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.IdPrescription, pm.IdMedicament });

        modelBuilder.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Prescription)
            .WithMany(pr => pr.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdPrescription);

        modelBuilder.Entity<PrescriptionMedicament>()
            .HasOne(pm => pm.Medicament)
            .WithMany(md => md.PrescriptionMedicaments)
            .HasForeignKey(pm => pm.IdMedicament);
        
        var doctors = new List<Doctor>
        {
            new Doctor {IdDoctor = 1, FirstName = "Janusz", LastName = "Kowalski", Email = "email@gmail.com"},
            new Doctor {IdDoctor = 2, FirstName = "Robert", LastName = "Lewandowski", Email = "emaillewego@rl9.com"},
            new Doctor {IdDoctor = 3, FirstName = "Popularny", LastName = "Doktor", Email = "email2@wp.com"}
        };

        var patients = new List<Patient>
        {
            new Patient {IdPatient = 1, FirstName = "Maciej", LastName = "Sigmiarski", Birthdate = new DateTime(1989, 9, 9)},
            new Patient {IdPatient = 2, FirstName = "Chory", LastName = "Pacjent", Birthdate = new DateTime(2002, 03, 27)},
            new Patient {IdPatient = 3, FirstName = "Kryśka", LastName = "Patrołowiczówna", Birthdate = new DateTime(2005, 05, 31)}
        };

        var medicaments = new List<Medicament>
        {
            new Medicament {IdMedicament = 1, Name = "APAP", Description = "Jakiś opis", Type = "Lekowy lek na leczenie" },
            new Medicament {IdMedicament = 2, Name = "IBupromSigma", Description = "Łagodzi objawy rizzu", Type = "Hustler"},
            new Medicament {IdMedicament = 3, Name = "BrzuchoMax", Description = "Brzuch boli...", Type = "Benben"}
        };

        var prescriptions = new List<Prescription>
        {
            new Prescription {IdPrescription = 1, Date = DateTime.Today, DueDate = new DateTime(2025, 5, 25), IdPatient = 1, IdDoctor = 1},
            new Prescription {IdPrescription = 2, Date = DateTime.Today, DueDate = new DateTime(2025, 05, 25), IdPatient = 2, IdDoctor = 1},
            new Prescription {IdPrescription = 3, Date = DateTime.Today, DueDate = new DateTime(2025, 05, 22), IdPatient = 1, IdDoctor = 3}
        };

        var prescriptionmedicaments = new List<PrescriptionMedicament>
        {
            new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 1, Dose = 1, Details = "Some details..." },
            new PrescriptionMedicament{IdPrescription = 2, IdMedicament = 2, Dose = 1337, Details = "Some rizz details..."},
            new PrescriptionMedicament {IdPrescription = 3, IdMedicament = 3, Dose = 10, Details = "Some details 2..."}
        };
        
        modelBuilder.Entity<Doctor>().HasData(doctors);
        modelBuilder.Entity<Medicament>().HasData(medicaments);
        modelBuilder.Entity<Patient>().HasData(patients);
        modelBuilder.Entity<Prescription>().HasData(prescriptions);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionmedicaments);
    }
}