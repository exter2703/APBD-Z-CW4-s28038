using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Z_CW4_s28038.Models;

[Table("PrescriptionMedicament")]
public class PrescriptionMedicament
{
    public int IdMedicament { get; set; }
    public int IdPrescription { get; set; }
    public int Dose { get; set; }
    [MaxLength(100)] public string Details { get; set; } = null!;
    
    public Medicament Medicament { get; set; }
    public Prescription Prescription { get; set; }
}