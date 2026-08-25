using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oto1.Models
{
    public class AppUserModel
    {
        [Key]
        public string UserId { get; set; }
        public string Password { get; set; }
        public byte UserRole { get; set; }
        public bool IsActive { get; set; }

    }
}
