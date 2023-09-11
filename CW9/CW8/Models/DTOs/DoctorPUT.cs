using System.ComponentModel.DataAnnotations;

namespace CW8.Models.DTOs
{
    public class DoctorPUT
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = null!;
    }
}
