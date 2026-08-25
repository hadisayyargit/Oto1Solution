using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auto1API.Models
{
    public class LocationAddressModel 
    {
        [Key]
        public long LocationAddressId { get; set; }
        public  string? FName { get; set; }
        public long? CustomerId { get; set; }
        public int? VendorId { get; set; }
        public int? PersonId { get; set; }
        public string? Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public byte[]? GeoLocation { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)] 
        public byte[]? Timestamp1 { get; set; }

    }

}
            