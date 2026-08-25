using MauiPersianToolkit;
using oto1.Models;
using oto1.Services;

namespace oto1;

public partial class PaymentPage : ContentPage
{
    public int SumNetAmount { get; set; }
    public int TotalAmount { get; set; }
    public int ExtraAmount { get; set; }
    public int ShippingAmount { get; set; }
    public int DiscountAmount { get; set; }
    public string DiscountCode { get; set; }
    public string AddressPname { get; set; }
    public string TicketDateTimeJalali { get; set; }
    public string VendorPname { get; set; }

    
        
    public int ItemCount { get; set; }

    CustomerServiceViewModel CustomerService { get; set; }
    public CustomerServiceController myCustomerServiceController { get; set; }

    public PaymentPage(long serviceid)
    {
        InitializeComponent();

        SetCustomerService(serviceid);

    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();


    }

    private async void btnPay_Clicked(object sender, EventArgs e)
    {
        try
        {
            //bool res = await myCustomerServiceController.UpdateCustomerServiceStatus(this.CustomerService.ServiceId, (byte)GlobalClass.enumServiceStatus.servicestatus_Payment);
            bool res = await myCustomerServiceController.UpdateCustomerService(new CustomerServiceModel() { ServiceId = this.CustomerService.ServiceId, DiscountCode = this.DiscountCode, DiscountAmount = this.DiscountAmount, ServiceDateTime = DateTime.Now, ExtraAmount = this.ExtraAmount, ShippingAmount = this.ShippingAmount, TotalAmount = this.TotalAmount, ServiceStatus = (byte)GlobalClass.enumServiceStatus.servicestatus_Payment });
            await DisplayAlert("پیام", "پرداخت با موفقیت انجام شد", "ok");

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
        }

    }

    private async void SetCustomerService(long serviceid)
    {
        this.myCustomerServiceController = new CustomerServiceController();
        List<CustomerServiceViewModel> customerserviceviewmodellist = new List<CustomerServiceViewModel>();
        CustomerServiceViewModel customerserviceviewmodel = new CustomerServiceViewModel();


        try
        {

            customerserviceviewmodellist = await myCustomerServiceController.GetCustomerService(serviceid);
            this.CustomerService = customerserviceviewmodellist.FirstOrDefault();

            ServiceController sc1 = new ServiceController();

           // LocationAddressModel loc1 = new LocationAddressModel();
           // loc1 = await sc1.GetLocationAddress(this.CustomerService.LocationAddressId);
            try
            {
                //this.AddressPname = loc1.FName;
                this.AddressPname = this.CustomerService.Address;
                this.TicketDateTimeJalali = this.CustomerService.TicketDateTime.ToPersianDateTime();
                this.VendorPname = this.CustomerService.VendorName;
                this.ExtraAmount = 100;
                this.ShippingAmount = 200;
                this.DiscountAmount=300;

                
            }
            catch
            {
                this.AddressPname = "";
                this.TicketDateTimeJalali = "";
                this.VendorPname ="";
                this.ExtraAmount = 0;
                this.ShippingAmount = 0;
                this.DiscountAmount = 0;
            }

            List<CustomerServiceItemViewModel> myItems = new List<CustomerServiceItemViewModel>();


            this.myCustomerServiceController = new CustomerServiceController();

            SumNetAmount = 0;


            try
            {
                myItems = await myCustomerServiceController.GetCustomerServiceItem(CustomerService.ServiceId);
                
                this.ItemCount = myItems.Count;
                
                if (myItems != null && myItems.Count > 0)
                {
                    foreach (var req in myItems)
                    {
                        SumNetAmount += req.Quantity * req.NetAmount;
                        req.ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/part/prt" + req.PartId.ToString() + ".png";

                    }
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("پیام", ex.Message, "ok");

            }
            collectionviewParts.ItemsSource = myItems;


            this.TotalAmount = this.SumNetAmount + this.ExtraAmount + this.ShippingAmount - this.DiscountAmount;

            BindingContext = this;
        }


        catch (Exception ex)
        {


        }

    }

    private void btnReturn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}