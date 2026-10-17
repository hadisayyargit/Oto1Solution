using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Auto1API.Models
{
    public class PartModel
    {
        [Key]
        public int PartId { get; set; }
        [MaxLength(50)]
        public required string PName { get; set; }

        [MaxLength(50)]
        public bool? IsActive { get; set; }
        public int? PartLevel1Id { get; set; }
        public int? PartLevel2Id { get; set; }
        public int? PartLevel3Id { get; set; }
        public int ManufacturerId { get; set; }
        public string? Viscosity { get; set; }
        public string? PerformanceLevel { get; set; }
        public string? Partcode { get; set; }
        public int? BatteryAmperage { get; set; }
        public int? BatteryVoltage { get; set; }
        public int? TireYear { get; set; }
        public string? TireWheels { get; set; }
        public string? TireRingSize { get; set; }
        public string? PartColor { get; set; }
        public string? PartAttribute { get; set; }

        public byte[]? Timestamp1 { get; set; }
        //public byte[]? ThumbnailPhoto { get; set; }
    }

   

}
