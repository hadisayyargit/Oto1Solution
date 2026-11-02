using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oto1.Models
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
