using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oto1.Models
{
    public class PartModel
    {
        [Key]
        public int PartId { get; set; }
        [MaxLength(50)]
        public string FName { get; set; }

        [MaxLength(50)]
        public string LName { get; set; }
        public int? PartLevel1Id { get; set; }
        public int? PartLevel2Id { get; set; }
        public int? PartLevel3Id { get; set; }
        public int ManufacturerId { get; set; }
        public string Viscosity { get; set; }
        public string PerformanceLevel { get; set; }
        public string? PartCode { get; set; }
        public string PhotoFileName { get; set; }
        public int? BatteryAmperage { get; set; }
        public int? BatteryVoltage { get; set; }
        public int? TireYear { get; set; }
        public string TireWheels { get; set; }
        public string TireRingSize { get; set; }
        public string PartColor { get; set; }
        public string PartAttribute { get; set; }
        public bool? IsActive { get; set; }
        public byte[] Timestamp1 { get; set; }
        public string ThumbnailPhotoFile { get; set; } 
    }


}
