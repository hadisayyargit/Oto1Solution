using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace oto1.Models
{
    public partial class PartCarVendorModel : PartModel, INotifyPropertyChanged
    {
        [Key]
        public long Id { get; set; }
        public long RankNo { get; set; }

        public int? CarId { get; set; }
        public int? VendorId { get; set; }
        public int PriceAmount { get; set; }
        public int NetAmount { get; set; }
        public int? Existance { get; set; }
        public string VendorName { get; set; }

        private int quantity;
        public int Quantity
        {
            get => quantity;
            set

            {
                if (quantity != value)
                {
                    quantity = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Quantity)));
                }
            }

        }
        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }

        public PartCarVendorModel()
        {
            IncreaseCommand = new Command(() => Quantity++);
            DecreaseCommand = new Command(() => Quantity--);
        }

        public event PropertyChangedEventHandler PropertyChanged;


    }


    /////////////////////////////////////////////////////
    
    /// <summary>
    /// 
    /// </summary>
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

        public string ThumbnailPhotoFile { get; set; }
     
    }
}
