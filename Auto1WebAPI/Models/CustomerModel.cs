using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auto1API.Models
{
    public class CustomerModel
    {
        [Key]
        public Int64 CustomerId { get; set; }
        public string? FName { get; set; }
        public required string LName { get; set; }
        public required string UserId { get; set; }
        public string? NationalCode{ get; set; }
        public string? Mobilephone { get; set; }       
       // public byte[]? ThumbnailPhoto { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[]? Timestamp1 { get; set; }
    }

    public class Customer_CarModel
    {

        [Key]
        public Int64 CustomerId { get; set; }
        public int CarId { get; set; }
        public required string FName { get; set; }
        public  string? LName { get; set; }
        public required string Plaque { get; set; }
        public bool ? IsActive { get; set; }
        //public string? ThumbnailPhoto { get; set; }
    }


}
