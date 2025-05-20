using APBD_Z_CW4_s28038.Data;
using APBD_Z_CW4_s28038.DTOs;
using APBD_Z_CW4_s28038.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Z_CW4_s28038.Services;

public interface IDbService
{
    public Task<IEnumerable<PatientGetDTO>> GetPatientDetails(int id);
    public Task<string> AddPrescription(PrescriptionAddDTO prescription);
}


public class DbService(MyDbContext data) : IDbService
{
    public async Task<IEnumerable<PatientGetDTO>> GetPatientDetails(int id)
    {
        return await data.Patients
            .Where(p=>p.IdPatient == id)
            .Select(st => new PatientGetDTO
            {
                IdPatient = st.IdPatient,
                FirstName = st.FirstName,
                LastName = st.LastName,
                Birthdate = st.Birthdate,
                Prescriptions = st.Prescriptions
                    .OrderBy(st => st.DueDate)
                    .Select(pre => new PatientGetPrescriptionsDTO
                {
                    IdPrescription = pre.IdPrescription,
                    Date = pre.Date,
                    DueDate = pre.DueDate,
                    Medicaments = pre.PrescriptionMedicaments
                        .Select(pm => new PatientGetMedicamentDTO
                    {
                        IdMedicament = pm.IdMedicament,
                        Name = pm.Medicament.Name,
                        Dose = pm.Dose,
                        Description = pm.Medicament.Description,
                    }),
                    Doctors = pre.PrescriptionMedicaments.Select(d => new PatientGetDoctorDTO
                    {
                        IdDoctor = pre.Doctor.IdDoctor,
                        FirstName = pre.Doctor.FirstName
                    })
                })
            }).ToListAsync();
    }

    public async Task<string> AddPrescription(PrescriptionAddDTO prescription)
    {
        if (prescription.Medicaments.Count > 10)
        {
            return "Recepta może zawierać max. 10 leków.";
        }

        if (prescription.DueDate < prescription.Date)
        {
            return "Data ważności nie może być wcześniejsza niż data wystawienia.";
        }

        var patient = await data.Patients.FindAsync(prescription.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                Birthdate = prescription.Patient.BirthDate,
            };
            data.Patients.Add(patient);
            await data.SaveChangesAsync();
        }

        var invalidMedicamentId = prescription.Medicaments
            .Where(m => !data.Medicaments.Any(db => db.IdMedicament == m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToList();

        if (invalidMedicamentId.Any())
        {
            return $"Nie znaleziono leku o Id: {string.Join(", ", invalidMedicamentId)}";
        }

        var prescript = new Prescription
        {
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = 1
        };
         data.Prescriptions.Add(prescript);
         await data.SaveChangesAsync();

         foreach (var med in prescription.Medicaments)
         {
             data.PrescriptionMedicaments.Add(new PrescriptionMedicament
             {
                 IdPrescription = prescript.IdPrescription,
                 IdMedicament = med.IdMedicament,
                 Dose = med.Dose,
                 Details = med.Description
             });
         }
         await data.SaveChangesAsync();
         return $"Recepta dla pacjenta: {patient.IdPatient} została poprawnie wystawiona.";
    }
}
