using System.ComponentModel.DataAnnotations;

namespace Auto1API.Models
{

    public class PartCarVendorModel
    {
        [Key]
        public long Id { get; set; }
        public long RankNo { get; set; }
        public int PartId { get; set; }
        [MaxLength(50)]
        public required string FName { get; set; }

        [MaxLength(50)]
        public string? LName { get; set; }
        public string? Viscosity { get; set; }
        public string? PerformanceLevel { get; set; }
        public int? CarId { get; set; }
        public int? VendorId { get; set; }
        public int? PriceAmount { get; set; }
        public int? NetAmount { get; set; }
        public int? Existance { get; set; }
        //public DateTime? ValidateBeginDate { get; set; }
        //public byte[]? ThumbnailPhoto { get; set; }
        public string? VendorName { get; set; }


    }
    public class PartVendorModel
    {
        [Key]
        public long Id { get; set; }
        public int PartId { get; set; }
        public int? VendorId { get; set; }

        public int? PriceAmount { get; set; }
        public byte? DiscountPercent { get; set; }
        public int? Existance { get; set; }
        public DateTime? ValidBeginDate { get; set; }
        public DateTime? ValidEndDate { get; set; }

        [MaxLength(50)]
        public required string FName { get; set; }

        [MaxLength(50)]
        public string? LName { get; set; }

        public string? jalaliBeginDate { get; set; }
        public string? JalaliEndDate { get; set; }


    }
}
