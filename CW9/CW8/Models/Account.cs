using System.ComponentModel.DataAnnotations;

namespace CW8.Models
{
    public class Account
    {
        [Key]
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
