using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace oto1.Models
{
    public class BasicPrimeModel
    {
        [Key]
        public int Id { get; set; }
        public required string GroupCode { get; set; }
        public int CodeValue { get; set; }
        
        [MaxLength(50)]
        public required string FName { get; set; }

        [MaxLength(50)]
        public required string LName { get; set; }
        public bool IsHeader { get; set; }
    }
}