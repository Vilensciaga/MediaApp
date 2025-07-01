
using Models.Models;
using System.ComponentModel.DataAnnotations;
using Models.ModelExtensions;



namespace MediaApp.Models
{
    public class AppUser
    {
        [Key]
        public int AppUserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateOfBirth { get; set; }
        [MaxLength(50)]
        public string KnownAs { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        [MaxLength(50)]
        public string Gender { get; set; }
        [MaxLength(5000)]
        public string Introduction { get; set; }
        [MaxLength(10000)]
        public string LookingFor { get; set; }
        [MaxLength(10000)]
        public string Interests { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }


        //this is making entity framework make exensive queries pulling all the data when we
        //project to our dto, we will configure it in the mapping instead
        //public int GetAge()
        //{
        //    return DateOfBirth.CalculateAge();
        //}

    }
}
