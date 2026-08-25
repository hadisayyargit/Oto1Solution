using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oto1.Models
{
    public class CustomerModel
    {
        [Key]
        public long CustomerId { get; set; }
        [MaxLength(50)]
        public string FName { get; set; }

        [MaxLength(50)]
        public string LName { get; set; }

        [MaxLength(50)]
        public string UserId { get; set; }

        [MaxLength(10)]
        public string NationalCode { get; set; }
        public string Mobilephone { get; set; }
        public DateTime BirthDate { get; set; }
        public byte[] Timestamp1 { get; set; }
        //public byte[] ThumbnailPhoto { get; set; }
    }

    public class Customer_CarModel
    {

        [Key]
        public long CustomerId { get; set; }
        public int CarId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Plaque { get; set; }
        //public bool IsActive { get; set; }
        public byte[] ThumbnailPhoto { get; set; }

        public string ThumbnailPhotoString { get; set; }
    }
}
