using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auto1API.Models
{
    public class AppUserModel
    {
        [Key]
        public  string UserId { get; set; }
        public  string Password { get; set; }
        public byte UserRole { get; set; }
        public bool IsActive { get; set; }
    }
}
