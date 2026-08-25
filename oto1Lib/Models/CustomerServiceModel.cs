using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Windows.Input;
using System.Xml.Linq;
using MauiPersianToolkit;

namespace oto1.Models
{
    public class CustomerServiceModel
    {
        [Key]
        public long ServiceId { get; set; }
        public long CustomerId { get; set; }
        public byte ServiceType { get; set; }
        public byte ServiceStatus { get; set; }
        public DateTime TicketDateTime { get; set; }
        public DateTime? ServiceDateTime { get; set; }
        public int VendorId { get; set; }
        public int? PostmanId { get; set; }
        public string Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? DiscountCode { get; set; }
        public int? DiscountAmount { get; set; }
        public int? ExtraAmount { get; set; }
        public int? ShippingAmount { get; set; }
        public int? TotalAmount { get; set; }
        public int DeliveryCode { get; set; }
        public string? ServicesDesc { get; set; }
        public byte[] Timestamp1 { get; set; }

    }
    public class CustomerServiceItemModel
    {
        [Key]
        public long ServiceItemId { get; set; }
        public long ServiceId { get; set; }
        public int PartId { get; set; }

        public byte ServiceItemType { get; set; }
        public byte ServiceItemStatus { get; set; }
        public int Quantity { get; set; }
        public int NetAmount { get; set; }
        public int PriceAmount { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] timestamp1 { get; set; }
    }
    public class CustomerServiceViewModel
    {
        [Key]
        public long ServiceId { get; set; }
        public long CustomerId { get; set; }
        public byte ServiceType { get; set; }
        public byte ServiceStatus { get; set; }
        public DateTime TicketDateTime { get; set; }
        public DateTime? ServiceDateTime { get; set; }
        public int ItemCount { get; set; }
        public int VendorId { get; set; }
        public int? PostmanId { get; set; }
        public string VendorName { get; set; }
        public string CustomerName { get; set; }
        public string? PostmanName { get; set; }
        public string ServiceTypeName { get; set; }
        public string ServiceStatusName { get; set; }
        public string Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? DiscountCode { get; set; }
        public int? DiscountAmount { get; set; }
        public int? ExtraAmount { get; set; }
        public int? ShippingAmount { get; set; }
        public int? TotalAmount { get; set; }
        public string CustomerThumbnailPhotoFile  => ("https://khordadnet.ir/mysites/oto1/assets/customer/c"+CustomerId.ToString()+".png");
    }
    public class CustomerServiceItemViewModel : INotifyPropertyChanged
    {
        public long ServiceId { get; set; }

        [Key]
        public long ServiceItemId { get; set; }

        public long CustomerId { get; set; }
        public byte ServiceType { get; set; }
        public byte ServiceStatus { get; set; }
        public DateTime TicketDateTime { get; set; }
        public DateTime? ServiceDateTime { get; set; }
        public string Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int PartId { get; set; }
        public byte ServiceItemType { get; set; }
        public byte ServiceItemStatus { get; set; }
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

        public CustomerServiceItemViewModel()
        {
            IncreaseCommand = new Command(() => Quantity++);
            DecreaseCommand = new Command(() => Quantity--);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string CheckboxTitle {get;set;}

        private bool isCheckBoxChecked = true;
        public bool IsCheckBoxChecked
        {
            get
            {
                CheckboxTitle = "موجود";
                return isCheckBoxChecked;
                
            }
            set
            {
                if (IsCheckBoxChecked != value)
                {
                    isCheckBoxChecked = value;
                    OnPropertyChanged(nameof(IsCheckBoxChecked)); 

                    if (value)
                        CheckboxTitle = "موجود";
                    else
                        CheckboxTitle = "ناموجود";
                    OnPropertyChanged(nameof(CheckboxTitle));
                }

             
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int NetAmount { get; set; }
        public int PriceAmount { get; set; }
        public string CustomerName { get; set; }
        public int? VendorId { get; set; }
        public int? PostmanId { get; set; }
        public string VendorName { get; set; }
        public string? PostmanName { get; set; }
        public string PartName { get; set; }
        public string ServiceTypeName { get; set; }
        public string ServiceStatusName { get; set; }
        public string ServiceItemTypeName { get; set; }
        public string ServiceItemStatusName { get; set; }

        public string ThumbnailPhotoFile { get; set; }

        //public byte[] ThumbnailPhoto { get; set; }

        //public string ThumbnailPhotoString { get; set; }

        public string tmp_OpacityControl { get; set; }
        public bool tmp_IsActiveControl { get; set; }
        public string tmp_Flag { get; set; }
    }


    public class CustomerServiceGroupModel : List<CustomerServiceItemViewModel>
    {
        public long ServiceId { get; set; }
        public DateTime TicketDateTime { get; set; }
        public string VendorName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string ServiceTypeName { get; set; }
        public string ServiceStatusName { get; set; }
        public string TicketDateTimeJalali {  get; set; }
        public int ItemCount { get; set; }
        public int? TotalAmount { get; set; }
        public string CustomerThumbnailPhotoFile { get; set; }


        public CustomerServiceGroupModel(long serviceid, DateTime ticketdatetime, string vendorname,string cutomername, string customeraddress, string servicetypename, string servicestatusname, int itemcount, int? totalamount, string customerthumbnailphotofile, List<CustomerServiceItemViewModel> customerserviceitemviewmodel) : base(customerserviceitemviewmodel)
        {
            ServiceId = serviceid;
            TicketDateTime = ticketdatetime;
            VendorName = vendorname;
            ServiceTypeName= servicetypename;
            ServiceStatusName = servicestatusname;
            TicketDateTimeJalali = TicketDateTime.ToPersianDateTime();
            TotalAmount = totalamount;
            ItemCount= itemcount;
            CustomerName = cutomername;
            CustomerAddress= customeraddress;
            CustomerThumbnailPhotoFile = customerthumbnailphotofile;
        }
    }
}
