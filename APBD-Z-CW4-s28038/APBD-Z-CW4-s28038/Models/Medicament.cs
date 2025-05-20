using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_Z_CW4_s28038.Models;

[Table("Medicaments")]
public class Medicament
{
    [Key] public int IdMedicament { get; set; }
    [MaxLength(100)] public string Name { get; set; } = null!;
    [MaxLength(100)] public string Description { get; set; } = null!;
    [MaxLength(100)] public string Type { get; set; } = null!;
    
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}