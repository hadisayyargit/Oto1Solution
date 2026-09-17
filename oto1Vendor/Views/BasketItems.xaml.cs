using MauiPersianToolkit;
using Microsoft.Maui.Controls;
using oto1.Models;
using oto1.Services;

namespace oto1;

public partial class BasketItems : ContentPage
{
    public long ServiceId { get; set; }
    public CustomerServiceController myCustomerServiceController { get; set; }
    List<CustomerServiceGroupModel> myGroupServices = new List<CustomerServiceGroupModel>();
    List<PersonModel> persons = new List<PersonModel>();
    ServiceController MyServiceController = new ServiceController();

    public BasketItems(long serviceid)
	{
		InitializeComponent();

        this.ServiceId = serviceid;
        RefreshForm();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        ServiceController MyServiceController = new ServiceController();

        persons = await MyServiceController.GetAllPersonsByUserRole((byte)GlobalClass.enumUserRole.userrole_postman);

        RefreshForm();
        
    }


    private async void RefreshForm()
    {
        activityIndicator.IsRunning = true;
        activityIndicator.IsVisible = true;

        pickerCourier.ItemsSource = persons;


        this.myCustomerServiceController = new CustomerServiceController();

        List<CustomerServiceViewModel> myOrders = new List<CustomerServiceViewModel>();
        List<CustomerServiceGroupModel> myGroupServices = new List<CustomerServiceGroupModel>();

        int vendorid = GlobalClass.m_VendorId;

        List<CustomerServiceItemViewModel> myOrdersItems = new List<CustomerServiceItemViewModel>();
        myOrdersItems = await myCustomerServiceController.GetCustomerServiceItem(this.ServiceId);
        if (myOrdersItems[0].ServiceStatus == (byte)GlobalClass.enumServiceStatus.servicestatus_Payment ||
            myOrdersItems[0].ServiceStatus == (byte)GlobalClass.enumServiceStatus.servicestatus_Pending)
        {
            btnSend.IsVisible = true;
            btnRefused.IsVisible = true;
            await myCustomerServiceController.UpdateCustomerServiceStatus(this.ServiceId, (byte)GlobalClass.enumServiceStatus.servicestatus_Pending);
        }
        else
        {
            btnSend.IsVisible = false;
            btnRefused.IsVisible = false;
        }

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


    private async void btnSend_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("هشدار", "سفارش ارسال شود؟", "بله", "خیر");

        if (answer)
        {
            if (pickerCourier.SelectedIndex<0)
            {
                await DisplayAlert("تایید", "لطفا پیک را انتخاب کن", "قبول");
                return;
            }

            var button = (Button)sender;
            // var servicesitems = (CustomerServiceGroupModel)button.BindingContext;

            var servicesitems = collectionviewBasket.ItemsSource;

            this.myCustomerServiceController = new CustomerServiceController();

            try
            {
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

                await myCustomerServiceController.UpdateCustomerServiceStatus(this.ServiceId, (byte)GlobalClass.enumServiceStatus.servicestatus_Sending);
                /// finding Nearset and available postman
                int postmanid = 4;
                await myCustomerServiceController.UpdateCustomerServicePostman(this.ServiceId, postmanid);


            }
            catch (Exception ex)
            {
            }
            await Navigation.PopAsync();
        }
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

    private async void btnRefused_Clicked(object sender, EventArgs e)
    {

        bool answer = await DisplayAlert("هشدار", "سفارش رد شود؟", "بله", "خیر");

        if (answer)
        {
            if (txtRemark.Text == ""||  txtRemark.Text ==null)
            {
                await DisplayAlert("تایید", "لطفا توضیحات را وارد کن", "قبول");
            }
            else
            {
                try
                {
                    await myCustomerServiceController.UpdateCustomerServiceStatus(this.ServiceId, (byte)GlobalClass.enumServiceStatus.servicestatus_Refusal);

                    await DisplayAlert("تایید", "سفارش رد شد", "قبول");
                    await Navigation.PopAsync();
                }
                catch
                {

                }
            }
        }


    }

    private void chkHasQuantity_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        //string s=e.Value.ToString();
          
    }
}