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
            new Doctor {IdDoctor = 1, FirstName = "Janusz", LastName = "Kowalski", Email = "email@gmail.com"}
        };

        var patients = new List<Patient>
        {
            new Patient
            {
                IdPatient = 1, FirstName = "Maciej", LastName = "Sigmiarski", Birthdate = new DateTime(1989, 9, 9)
            },
        };

        var medicaments = new List<Medicament>
        {
            new Medicament
                { IdMedicament = 1, Name = "APAP", Description = "Jakiś opis", Type = "Lekowy lek na leczenie" }
        };

        var prescriptions = new List<Prescription>
        {
            new Prescription { IdPrescription = 1, Date = DateTime.Today, DueDate = new DateTime(2025, 5, 25), IdPatient = 1, IdDoctor = 1}
        };

        var prescriptionmedicaments = new List<PrescriptionMedicament>
        {
            new PrescriptionMedicament { IdPrescription = 1, IdMedicament = 1, Dose = 1, Details = "Some details..." }
        };
        
        modelBuilder.Entity<Doctor>().HasData(doctors);
        modelBuilder.Entity<Medicament>().HasData(medicaments);
        modelBuilder.Entity<Patient>().HasData(patients);
        modelBuilder.Entity<Prescription>().HasData(prescriptions);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionmedicaments);
    }
}