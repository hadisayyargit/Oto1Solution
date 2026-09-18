using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oto1.Models
{
    public class PersonModel
    {
        [Key]
        public int PersonId { get; set; }
        public string FName { get; set; }
        public string? LName { get; set; }
        public string? UserId { get; set; }
        public string? Mobilephone { get; set; }
        public string? NationalCode { get; set; }
        public string? Email { get; set; }
        public byte[]? ThumbnailPhoto { get; set; }
        public byte[]? Timestamp1 { get; set; }
        public string FullName => $"{FName} {LName}";



    }
}
