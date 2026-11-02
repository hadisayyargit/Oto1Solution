using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Auto1API.Models
{
    public class PartLevelModel
    {
        [Key]
        public int PartLevel1Id { get; set; }
        public int PartLevel2Id { get; set; }
        public int PartLevel3Id { get; set; }
        
        
        [MaxLength(50)]
        public required string PName { get; set; }

        [MaxLength(50)]
        public string? LName { get; set; }

    }

   

}
