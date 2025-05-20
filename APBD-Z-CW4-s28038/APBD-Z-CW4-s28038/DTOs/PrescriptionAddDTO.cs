using APBD_Z_CW4_s28038.Models;

namespace APBD_Z_CW4_s28038.DTOs;

public class PrescriptionAddDTO
{
    public PatientDTO Patient { get; set; } = null!;
    public List<PrescriptionMedicamentDTO> Medicaments { get; set; } = new();
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}

public class PatientDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
}

public class PrescriptionMedicamentDTO
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; } = null!;
}