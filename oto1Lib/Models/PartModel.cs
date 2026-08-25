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
        public string? Barcode { get; set; }
        public string PhotoFileName { get; set; }

        public bool? IsActive { get; set; }
        public byte[] Timestamp1 { get; set; }
        public string ThumbnailPhotoFile { get; set; } 
    }


}
