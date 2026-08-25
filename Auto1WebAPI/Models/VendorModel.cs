using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auto1API.Models
{
    public class VendorModel
    {
        [Key]
        public int VendorId { get; set; }
        public required string FName { get; set; }
        public string? LName { get; set; }
        public string? UserId { get; set; }
        public string? Telephone { get; set; }
        public string? Mobilephone { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[]? Timestamp1 { get; set; }
        
    }
}
