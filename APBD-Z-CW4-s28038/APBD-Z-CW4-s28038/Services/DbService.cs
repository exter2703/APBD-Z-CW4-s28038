using APBD_Z_CW4_s28038.Data;
using APBD_Z_CW4_s28038.DTOs;
using APBD_Z_CW4_s28038.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_Z_CW4_s28038.Services;

public interface IDbService
{
    public Task<IEnumerable<PatientGetDTO>> GetPatientDetails(int id);
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
}
