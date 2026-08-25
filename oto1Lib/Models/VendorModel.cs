using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oto1.Models
{
    public class VendorModel
    {
        [Key]
        public int VendorId { get; set; }
        public  string FName { get; set; }
        public string? LName { get; set; }
        public string UserId { get; set; }
        public string Telephone { get; set; }
        public string Mobilephone { get; set; }
        public byte[] ThumbnailPhoto { get; set; }
        public byte[] Timestamp1 { get; set; }

    }
}
