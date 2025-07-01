using MediaApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        [MaxLength(300)]
        public string Url { get; set; }

        public bool IsMain { get; set; }
        [MaxLength(40)]
        public string PublicId { get; set; }

        //ensures 1 to many relationship
        public AppUser AppUser { get; set; }
        //foreign key
        public int AppUserId { get; set; }
    }
}