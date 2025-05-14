
using System.ComponentModel.DataAnnotations;

namespace MediaApp.Models
{
    public class AppUser
    {
        [Key]
        public int AppUserId { get; set; }
        public string UserName { get; set; } = string.Empty;

    }
}
