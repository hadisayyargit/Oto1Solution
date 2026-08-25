using MauiPersianToolkit;
using Microsoft.Maui.Controls;
using oto1.Models;
using oto1.Services;
using System.Numerics;

namespace oto1;

public partial class BasketItems : ContentPage
{
    public long ServiceId { get; set; }
    public CustomerServiceController myCustomerServiceController { get; set; }
    List<CustomerServiceGroupModel> myGroupServices = new List<CustomerServiceGroupModel>();

    public BasketItems(long serviceid)
    {
        InitializeComponent();

        this.ServiceId = serviceid;
        RefreshForm();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        RefreshForm();
    }


    private async void RefreshForm()
    {
        activityIndicator.IsRunning = true;
        activityIndicator.IsVisible = true;


        this.myCustomerServiceController = new CustomerServiceController();

        List<CustomerServiceViewModel> myOrders = new List<CustomerServiceViewModel>();
        List<CustomerServiceGroupModel> myGroupServices = new List<CustomerServiceGroupModel>();
        List<CustomerServiceItemViewModel> myOrdersItems = new List<CustomerServiceItemViewModel>();

        myOrdersItems = await myCustomerServiceController.GetCustomerServiceItem(this.ServiceId);

        foreach (var item in myOrdersItems)
        {
            item.ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/part/prt" + item.PartId.ToString() + ".png";
        }

        collectionviewBasket.ItemsSource = myOrdersItems;

        activityIndicator.IsRunning = false;
        activityIndicator.IsVisible = false;
    }


    private void TapNavigateBack_Tapped(object sender, TappedEventArgs e)
    {
        Navigation.PopAsync();
    }


    private async void btnNext_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        // var servicesitems = (CustomerServiceGroupModel)button.BindingContext;

        var servicesitems = collectionviewBasket.ItemsSource;

        this.myCustomerServiceController = new CustomerServiceController();

        try
        {

            /*
            foreach (CustomerServiceItemViewModel serviceitem in servicesitems)
            {

                try
                {
                    if (serviceitem.IsCheckBoxChecked)
                    {
                        await myCustomerServiceController.UpdateCustomerServiceItemStatus(serviceitem.ServiceItemId, (byte)GlobalClass.enumServiceItemStatus.serviceitemstatus_Done);

                    }
                    else
                    {
                        await myCustomerServiceController.UpdateCustomerServiceItemStatus(serviceitem.ServiceItemId, (byte)GlobalClass.enumServiceItemStatus.serviceitemstatus_Noexist);
                    }
                }
                catch
                {

                }
            }
            */


            List<CustomerServiceViewModel> myOrders = new List<CustomerServiceViewModel>();
            myOrders = await myCustomerServiceController.GetCustomerService(this.ServiceId);
            CustomerServiceViewModel newcustomerServiceView = myOrders.FirstOrDefault();

            CustomerServiceModel newcustomerServiceModel = new CustomerServiceModel() { ServiceId = this.ServiceId, DeliveryCode=(string.IsNullOrEmpty(txtDeliveryCode.Text) ? 0: int.Parse(txtDeliveryCode.Text)), ServicesDesc=txtServicesDesc.Text, DiscountCode = newcustomerServiceView.DiscountCode, DiscountAmount = newcustomerServiceView.DiscountAmount, ServiceDateTime = DateTime.Now, ExtraAmount = newcustomerServiceView.ExtraAmount, ShippingAmount = newcustomerServiceView.ShippingAmount, TotalAmount = newcustomerServiceView.TotalAmount, ServiceStatus = (byte)GlobalClass.enumServiceStatus.servicestatus_Done };

            bool res = await myCustomerServiceController.UpdateCustomerService(newcustomerServiceModel);


        }
        catch (Exception ex)
        {
        }
        await Navigation.PopAsync();
    }
    //private async void btnUpdateServiceItem_Clicked(object sender, EventArgs e)
    //{
    //    var button = (Button)sender;
    //    var item = (CustomerServiceItemViewModel)button.BindingContext;
    //    long id = item.ServiceItemId;
    //    int qty = item.Quantity;
    //    try
    //    {
    //        bool res = await myCustomerServiceController.UpdateCustomerServiceItemQuantity(id, qty);
    //        await DisplayAlert("تایید", "بروزرسانی شد", "قبول");
    //    }
    //    catch
    //    {

    //    }
    //}

    private async void btnReject_Clicked(object sender, EventArgs e)
    {

        bool answer = await DisplayAlert("هشدار", "سفارش برگشت خورد؟", "بله", "خیر");


        if (answer)
        {
            try
            {
                List<CustomerServiceViewModel> myOrders = new List<CustomerServiceViewModel>();
                myOrders = await myCustomerServiceController.GetCustomerService(this.ServiceId);
                CustomerServiceViewModel newcustomerServiceView = myOrders.FirstOrDefault();

                CustomerServiceModel newcustomerServiceModel = new CustomerServiceModel() { ServiceId = this.ServiceId,  ServicesDesc = txtServicesDesc.Text, DiscountCode = newcustomerServiceView.DiscountCode, DiscountAmount = newcustomerServiceView.DiscountAmount, ServiceDateTime = DateTime.Now, ExtraAmount = newcustomerServiceView.ExtraAmount, ShippingAmount = newcustomerServiceView.ShippingAmount, TotalAmount = newcustomerServiceView.TotalAmount, ServiceStatus = (byte)GlobalClass.enumServiceStatus.servicestatus_Reject };

                bool res = await myCustomerServiceController.UpdateCustomerService(newcustomerServiceModel);
                //await myCustomerServiceController.UpdateCustomerServiceStatus(this.ServiceId, (byte)GlobalClass.enumServiceStatus.servicestatus_Refusal);

                await DisplayAlert("تایید", "سفارش توسط مشتری تحویل و پذیرفته نشد", "قبول");
                await Navigation.PopAsync();
            }
            catch
            {

            }
        }


    }

    private void chkHasQuantity_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        //string s=e.Value.ToString();

    }
}