using System.ComponentModel.DataAnnotations;

namespace Models.Dtos.User
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public bool IsMain { get; set; }
    }
}