using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace oto1.Models
{
    public class CityModel
    {
        [Key]
        public int CityId { get; set; }
        [MaxLength(50)]
        public  string CityName { get; set; }
        public int ProvinceId { get; set; }
        public  string ProvinceName { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }

    public class ProvinceModel
    {
        [Key]
        public int ProvinceId { get; set; }
        public  string ProvinceName { get; set; }
    }

}
