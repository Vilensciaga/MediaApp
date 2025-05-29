
using System.ComponentModel.DataAnnotations;

namespace MediaApp.Models
{
    public class AppUser
    {
        [Key]
        public int AppUserId { get; set; }

        [Required]
        [MaxLength(40)]
        public string UserName { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
