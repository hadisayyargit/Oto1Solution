using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auto1API.Models
{
    public class CustomerServiceModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ServiceId { get; set; }
        public long CustomerId { get; set; }
        public byte ServiceType { get; set; }
        public byte ServiceStatus { get; set; }
        public DateTime TicketDateTime { get; set; }
        public DateTime? ServiceDateTime { get; set; }
        public string? Plaque { get; set; }
        public int VendorId { get; set; }
        public int? PostmanId { get; set; }
        public string? Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? DiscountCode { get; set; }
        public int? DiscountAmount { get; set; }
        public int? ExtraAmount { get; set; }
        public int? ShippingAmount { get; set; }
        public int? TotalAmount { get; set; }
        public int? DeliveryCode { get;set; }
        public string? ServicesDesc { get; set; }
        

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[]? Timestamp1 { get; set; }
    }


    public class CustomerServiceItemModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ServiceItemId { get; set; }
        public long ServiceId { get; set; }
        public int? PartId { get; set; }
        public byte ServiceItemType { get; set; }
        public byte ServiceItemStatus { get; set; }
        public int Quantity { get; set; }
        public int NetAmount { get; set; }
        public int PriceAmount { get; set; }
        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[]? timestamp1 { get; set; }
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

        public int CountItems { get; set; }
        public int SumNetAmount { get; set; }
        public int SumPriceAmount { get; set; }
        public int VendorId { get; set; }
        public int? PostmanId { get; set; }
        public string? VendorName { get; set; }
        public string? CustomerName { get; set; }
        public string? PostmanName { get; set; }
        public string? ServiceTypeName { get; set; }
        public string? ServiceStatusName { get; set; }
        public string? Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? DiscountCode { get; set; }
        public int? DiscountAmount { get; set; }
        public int? ExtraAmount { get; set; }
        public int? ShippingAmount { get; set; }
        public int? TotalAmount { get; set; }
        public int? DeliveryCode { get; set; }

    }

    public class CustomerServiceItemViewModel
    {
        public long ServiceId { get; set; }

        [Key]
        public long ServiceItemId { get; set; }

        public long CustomerId { get; set; }
        public byte ServiceType { get; set; }
        public byte ServiceStatus { get; set; }
        public DateTime TicketDateTime { get; set; }
        public DateTime? ServiceDateTime { get; set; }
        public string? Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public int? PartId { get; set; }
        public byte ServiceItemType { get; set; }
        public byte ServiceItemStatus { get; set; }
        public int Quantity { get; set; }
        public int NetAmount { get; set; }
        public int PriceAmount { get; set; }
        public int VendorId { get; set; }
        public int? PostmanId { get; set; }
        public string? CustomerName { get; set; }
        public string? VendorName { get; set; }
        public string? PostmanName { get; set; }
        public string? PartName { get; set; }
        public string? ServiceTypeName { get; set; }
        public string? ServiceStatusName { get; set; }
        public string? ServiceItemTypeName { get; set; }
        public string? ServiceItemStatusName { get; set; }

    }


    public class CustomerServiceDummyModel
    {
        [Key]
        public long ServiceId { get; set; }
    }


}
