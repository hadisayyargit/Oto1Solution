using System.ComponentModel.DataAnnotations;

namespace Auto1API.Models
{
    public class CarModel
    {

        [Key]
        public int CarId { get; set; }
        public required string FName { get; set; }        
        public required string LName { get; set; }
        public string? CarGroup { get; set; }
        public int? CarGroupId { get; set; }
        public string? Brand { get; set; }
        public string? Company { get; set; }
        public int? VehicleType { get; set; }
        public string? ThumbnailPhotoFile { get; set; }
       
    }
}
