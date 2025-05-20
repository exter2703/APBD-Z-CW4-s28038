using APBD_Z_CW4_s28038.Models;

namespace APBD_Z_CW4_s28038.DTOs;

public class PatientGetDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public IEnumerable<PatientGetPrescriptionsDTO> Prescriptions { get; set; }
}

public class PatientGetPrescriptionsDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public IEnumerable<PatientGetMedicamentDTO>? Medicaments { get; set; }
    public IEnumerable<PatientGetDoctorDTO>? Doctors { get; set; }
}

public class PatientGetMedicamentDTO
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; }
}

public class PatientGetDoctorDTO
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
}