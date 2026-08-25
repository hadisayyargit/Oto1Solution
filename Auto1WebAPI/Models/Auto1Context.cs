using Microsoft.EntityFrameworkCore;

namespace Auto1API.Models
{
    public class Auto1Context:DbContext
    {
        public Auto1Context(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }
        public DbSet<CustomerModel> Customer { get; set; }
        public DbSet<CarModel> Car { get; set; }
        public DbSet<PartModel>Part  { get; set; }
        public DbSet<ManufacturerModel> Manufacturer { get; set; }
        public DbSet<VendorModel> Vendor { get; set; }
        public DbSet<LocationAddressModel> LocationAddress { get; set; } = null!;

        public DbSet<Customer_CarModel> Customer_Car { get; set; } = null!;

        public DbSet<PartCarVendorModel> ProductCarVendor { get; set; } = null!;
        public DbSet<PartVendorModel> PartVendor{ get; set; } = null!;

        public DbSet<CustomerServiceModel> CustomerService { get; set; } = null!;
        public DbSet<CustomerServiceItemModel> CustomerServiceItem { get; set; } = null!;
        public DbSet<CustomerServiceViewModel> CustomerServiceView { get; set; } = null!;
        public DbSet<CustomerServiceItemViewModel> CustomerServiceItemView { get; set; } = null!;
        public DbSet<CustomerServiceDummyModel> CustomerServiceDummy { get; set; } = null!;
        public DbSet<AppUserModel> AppUser { get; set; }
        public DbSet<PersonModel> Person { get; set; }
    }
}
