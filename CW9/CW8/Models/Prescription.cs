using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW8.Models
{
    public class Prescription
    {
        [Key]
        public int IdPrescription { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public int IdDoctor { get; set; }
        [Required]
        public int IdPatient { get; set; }
        public virtual ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; } = null!;
        [ForeignKey(nameof(IdDoctor))]
        public virtual Doctor Doctor { get; set; } = null!;
        [ForeignKey(nameof(IdPatient))]
        public virtual Patient Patient { get; set; } = null!;
    }
}
