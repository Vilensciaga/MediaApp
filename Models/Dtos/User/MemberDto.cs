using Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos.User
{
    public class MemberDto
    {
        public int AppUserId { get; set; }

        public string Username { get; set; }
        public string? PhotoUrl { get; set; }
        //Automapper will automatically map age to the GetAge method because of the naming convention
        public int? Age{ get; set; }
        public string? KnownAs { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastActive { get; set; }
        public string? Gender { get; set; }
        public string? Introduction { get; set; }
        public string? LookingFor { get; set; }
        public string? Interests { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public ICollection<PhotoDto>? Photos { get; set; }
    }
}
