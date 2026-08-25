using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auto1API.Models
{
    public class PersonModel
    {
        [Key]
        public int PersonId { get; set; }
        public required string FName { get; set; }
        public string? LName { get; set; }
        public string? UserId { get; set; }
        
        public string? Mobilephone { get; set; }
        public string? NationalCode { get; set; }
        public string? Email { get; set; }

    }
}
