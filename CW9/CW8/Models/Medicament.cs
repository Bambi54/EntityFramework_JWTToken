using System.ComponentModel.DataAnnotations;

namespace CW8.Models
{
    
    public class Medicament
    {
        [Key]
        public int IdMedicament { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string Type { get; set; } = null!;
        public virtual ICollection<Prescription_Medicament> Prescription_Medicaments { get; set; } = null!;
    }
}
