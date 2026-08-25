using MauiPersianToolkit;
using Microsoft.Maui.Controls;
using oto1.Models;
using oto1.Services;

namespace oto1;

public partial class Baskets : ContentPage
{

    public CustomerServiceController myCustomerServiceController { get; set; }
    List<CustomerServiceGroupModel> myGroupServices = new List<CustomerServiceGroupModel>();

    public Baskets()
	{
		InitializeComponent();
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

        long serviceid = -1, customerid = GlobalClass.m_CustomerId;
        int vendorid = -1;
        int postmanid = -1;

        string beginticketdatetime = "2024-01-01";
        string endticketdatetime = DateTime.Today.ToString("yyyy-MM-dd");

        string servicestatusserie = ((byte)GlobalClass.enumServiceStatus.servicestatus_Ticketing).ToString();

        try
        {
            myOrders = await myCustomerServiceController.GetCustomerService(serviceid, customerid, vendorid, postmanid,beginticketdatetime, endticketdatetime, servicestatusserie);

        }
        catch (Exception ex)
        {
            await DisplayAlert("پیام", ex.Message, "ok");

        }

        if (myOrders != null && myOrders.Count > 0)
        {
            foreach (var req in myOrders)
            {
                List<CustomerServiceItemViewModel> myOrdersItems = new List<CustomerServiceItemViewModel>();
                myOrdersItems = await myCustomerServiceController.GetCustomerServiceItem(req.ServiceId);
                foreach (var item in myOrdersItems)
                {
                    item.ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/part/prt" + item.PartId.ToString() + ".png";
                }

                myGroupServices.Add(new CustomerServiceGroupModel(req.ServiceId,req.TicketDateTime,req.VendorName,"","",req.ServiceTypeName,req.ServiceStatusName,req.ItemCount,req.TotalAmount,"", myOrdersItems));
            }
        }



        collectionviewBasket.ItemsSource = myGroupServices;
       
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
        var servicesitems = (CustomerServiceGroupModel)button.BindingContext;

        var myCustomerServiceGroup = servicesitems.ToList().FirstOrDefault();
        long id = servicesitems.ServiceId;

        this.myCustomerServiceController = new CustomerServiceController();

        foreach (var serviceitem in servicesitems)
        {
     

            try
            {
                if (serviceitem.Quantity == 0)
                {
                    bool res = await myCustomerServiceController.DeleteCustomerServiceItem(serviceitem.ServiceItemId);
                    List<CustomerServiceItemViewModel> newitems =  await myCustomerServiceController.GetCustomerServiceItem(id);
                    if (newitems.Count == 0)
                    {
                        RefreshForm();
                        return;
                    }

                }
                else
                {


                    bool res = await myCustomerServiceController.UpdateCustomerServiceItemQuantity(serviceitem.ServiceItemId, serviceitem.Quantity);
                    //await DisplayAlert("تایید", "بروزرسانی شد", "قبول");
                }
            }
            catch
            {

            }
        }
        

        await Navigation.PushAsync(new PaymentPage(id));
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

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {


        var button = (Button)sender;
        var servicesitems = (CustomerServiceGroupModel)button.BindingContext;

        var myCustomerServiceGroup = servicesitems.ToList().FirstOrDefault();
        long id = servicesitems.ServiceId;


        bool answer = await DisplayAlert("هشدار", "سبد حذف شود؟", "بله", "خیر");
        

            if (answer)
            {
            try
            {
                bool res = await myCustomerServiceController.DeleteCustomerService(id);
                await DisplayAlert("تایید", "سبد حذف شد", "قبول");
                RefreshForm();
            }
            catch
            {

            }
            }
        
    
    }
}