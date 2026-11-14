using System.ComponentModel.DataAnnotations;

namespace oto1API.Models
{
    public class CityModel
    {
        [Key]
        public int CityId { get; set; }
        [MaxLength(50)]
        public required string CityName { get; set; }
        public int ProvinceId { get; set; }
        public required string ProvinceName { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }

    public class ProvinceModel
    {
        [Key]
        public int ProvinceId { get; set; }
        public required string ProvinceName { get; set; }
    }

}
