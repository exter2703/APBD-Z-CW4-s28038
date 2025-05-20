using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Z_CW4_s28038.Models;

[Table("Prescription")]
public class Prescription
{
    [Key] public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    
    [ForeignKey("IdPatient")]
    public Patient Patient { get; set; } = null!;
    [ForeignKey("IdDoctor")]
    public Doctor Doctor { get; set; } = null!;
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}