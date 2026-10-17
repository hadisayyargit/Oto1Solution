using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oto1.Models
{
    public partial class ManufacturerModel
    {
        [Key]
        public int ManufacturerId { get; set; }
        public required string PName { get; set; }
        public string PhotoFileName { get; set; }
    }
}
