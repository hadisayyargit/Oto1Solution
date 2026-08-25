using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Auto1API.Models
{
    public class PartModel
    {
        [Key]
        public int PartId { get; set; }
        [MaxLength(50)]
        public required string FName { get; set; }

        [MaxLength(50)]
        public string? LName { get; set; }
        public bool? IsActive { get; set; }
        public int? PartLevel1Id { get; set; }
        public int? PartLevel2Id { get; set; }
        public int? PartLevel3Id { get; set; }
        public int ManufacturerId { get; set; }
        public string? Viscosity { get; set; }
        public string? PerformanceLevel { get; set; }
        public string? Barcode { get; set; }

        public byte[]? Timestamp1 { get; set; }
        //public byte[]? ThumbnailPhoto { get; set; }
    }

   

}
