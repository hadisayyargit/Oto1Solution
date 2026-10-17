using System.ComponentModel.DataAnnotations;

namespace Auto1API.Models
{
    public class ManufacturerModel
    {
        [Key] 
        public int ManufacturerId { get; set; } 
        public required string PName { get; set; }
        public string? PhotoFileName { get; set; }       

    }
}
