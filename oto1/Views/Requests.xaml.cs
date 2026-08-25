namespace oto1;

using oto1.Models;
using oto1.Services;
using Microsoft.Maui.Controls;
using oto1;
using Microsoft.Maui;
using System.Diagnostics;

public partial class Requests : ContentPage
{
    public ServiceController MyServiceController { get; set; }
    public CustomerServiceController myCustomerServiceController { get; set; }
    List<CustomerServiceItemViewModel> mySaleServices = new List<CustomerServiceItemViewModel>();
    List<CustomerServiceItemViewModel> myLocationServices = new List<CustomerServiceItemViewModel>();
    List<CustomerServiceItemViewModel> mySiteServices = new List<CustomerServiceItemViewModel>();
    List<CustomerServiceItemViewModel> myTowingServices = new List<CustomerServiceItemViewModel>();
    public Requests()
	{
		InitializeComponent();


        mySaleServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "خرید روغن موتور", ThumbnailPhotoFile = "oil3.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_Part, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_PartSale, tmp_Flag = "lubricant", tmp_OpacityControl = "1", tmp_IsActiveControl = true });
        mySaleServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "خرید قطعه و لوازم یدکی", ThumbnailPhotoFile = "sparkplug.png" , ServiceType = (byte)GlobalClass.enumServiceType.servicetype_Part, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_PartSale, tmp_Flag = "part", tmp_OpacityControl = "0.5", tmp_IsActiveControl = false });

        myLocationServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "تعویض روغن", ThumbnailPhotoFile = "oil2.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnLocationService, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_LubricantService, tmp_OpacityControl = "0.5", tmp_IsActiveControl = false });
        myLocationServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "تعویض باتری", ThumbnailPhotoFile = "carbattery2.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnLocationService, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_Battery,  tmp_OpacityControl = "0.5", tmp_IsActiveControl = false });
        myLocationServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "سوخت رسانی", ThumbnailPhotoFile = "fuel1.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnLocationService, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_Fuel,  tmp_OpacityControl = "0.5", tmp_IsActiveControl = false });
        myLocationServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "پنچرگیری", ThumbnailPhotoFile = "tirepuncture2.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnLocationService, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_tirepuncture,  tmp_OpacityControl = "0.5", tmp_IsActiveControl = false });
        myLocationServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "تعویض تسمه تایم", ThumbnailPhotoFile = "timingbelt2.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnLocationService, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_timingbelt,  tmp_OpacityControl = "0.5", tmp_IsActiveControl = false });
        myLocationServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "برق خودرو", ThumbnailPhotoFile = "carbattery2.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnLocationService, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_carwire, tmp_OpacityControl = "0.5", tmp_IsActiveControl = false });
        myLocationServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "عیب یابی", ThumbnailPhotoFile = "cartroubleshooting.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnLocationService, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_troubleshooting, tmp_OpacityControl = "0.5", tmp_IsActiveControl = false });

        mySiteServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "تعویض روغن", ThumbnailPhotoFile = "oil1.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnSiteService, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_LubricantService, tmp_OpacityControl = "0.5", tmp_IsActiveControl = false });
        mySiteServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "تعویض باتری", ThumbnailPhotoFile = "carbattery.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnSiteService, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_Battery,  tmp_OpacityControl = "0.5", tmp_IsActiveControl = false});
        mySiteServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "پنچرگیری", ThumbnailPhotoFile = "tirepuncture1.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnSiteService, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_tirepuncture,  tmp_OpacityControl = "0.5", tmp_IsActiveControl = false});
        mySiteServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "تعویض تسمه تایم", ThumbnailPhotoFile = "timingbelt2.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnSiteService, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_timingbelt, tmp_OpacityControl = "0.5", tmp_IsActiveControl = false });
        mySiteServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "شستشوی خودرو", ThumbnailPhotoFile = "carwash1.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnSiteService, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_carwash,  tmp_OpacityControl = "0.5", tmp_IsActiveControl = false});
        mySiteServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "برق خودرو", ThumbnailPhotoFile = "carbattery2.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnSiteService, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_carwire,  tmp_OpacityControl = "0.5", tmp_IsActiveControl = false});
        mySiteServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "عیب یابی", ThumbnailPhotoFile = "cartroubleshooting.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnSiteService, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_troubleshooting, tmp_OpacityControl = "0.5", tmp_IsActiveControl = false});


        myTowingServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "حمل خودرو سبک", ThumbnailPhotoFile = "towtruck2.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_CarTowing, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_towing_light, tmp_OpacityControl = "0.5", tmp_IsActiveControl = false});
        myTowingServices.Add(new CustomerServiceItemViewModel() { ServiceItemTypeName = "حمل خودرو سنگین", ThumbnailPhotoFile = "towtruck3.png", ServiceType = (byte)GlobalClass.enumServiceType.servicetype_CarTowing, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_towing_Heavy,  tmp_OpacityControl = "0.5", tmp_IsActiveControl = false});


        listviewServiceItemType.ItemsSource = mySaleServices;


    }
    
    protected override  void OnAppearing()
    {
        base.OnAppearing();


        if (GlobalClass.m_CustomerServiceItemView == null)
        {
            GlobalClass.m_CustomerServiceItemView = new CustomerServiceItemViewModel();
            GlobalClass.m_CustomerServiceItemView.ServiceType = (byte)GlobalClass.enumServiceType.servicetype_Part;

        }

        switch (GlobalClass.m_CustomerServiceItemView.ServiceType)
            {
                case (byte)GlobalClass.enumServiceType.servicetype_NoSelect:
                    break;
                case (byte)GlobalClass.enumServiceType.servicetype_Part:
                    rdoPart.IsChecked = true;
                    break;
                case (byte)GlobalClass.enumServiceType.servicetype_OnLocationService:
                    rdoLocationService.IsChecked = true;
                    break;
                case (byte)GlobalClass.enumServiceType.servicetype_OnSiteService:
                    rdoSiteService.IsChecked = true;    
                    break;
                case (byte)GlobalClass.enumServiceType.servicetype_CarTowing:
                    rdoTowing.IsChecked = true;
                    break;
            }
        //RefreshForm();
    }



    /*
    private async void btnCarPart_Clicked(object sender, EventArgs e)
    {
        //var toolbarItem = this.FindByName<ToolbarItem>("toolbaritemBasketCount");
        //toolbarItem.Text = "5";


        ///updating appshell toolbar
        MessagingCenter.Send(this, "UpdateToolbarItem", "8");



        if (NetClass.CheckNetConnection() == false)
        {
            await DisplayAlert("پیام", "Internet Connection Fail", "ok");
        }

        this.MyServiceController = new ServiceController();

        try
        {
            List<CustomerModel> mycustomerlist = await MyServiceController.GetAllCustomers();

            if (mycustomerlist != null && mycustomerlist.Count > 0)
            {
                string s = "";

                s = mycustomerlist[0].FName;

                //await DisplayAlert("", mycustomerlist.Count.ToString(), "ok");
                await DisplayAlert("", s, "ok");

            }
            else
            {
                await DisplayAlert("", "Empty List", "ok");
            }
        }
        catch (Exception ex)
        {
        }
    }


    private async void RefreshForm()
    {
        //activityIndicator.IsRunning = true;

        this.MyServiceController = new ServiceController();
        this.myCustomerServiceController = new CustomerServiceController();

        List<CustomerServiceViewModel> myOrders = new List<CustomerServiceViewModel>();
        long serviceid = -1, customerid = GlobalClass.m_CustomerId;
        int vendorid = -1;
        string ticketdatetime = DateTime.Today.ToString("yyyy-MM-dd");
       // ticketdatetime = "2024-10-18";
        byte servicestatus = 0;

        try
        {
            myOrders = await myCustomerServiceController.GetCustomerService(serviceid, customerid, vendorid, ticketdatetime, ticketdatetime, servicestatus);

        }
        catch (Exception ex)
        {
            await DisplayAlert("پیام", ex.Message, "ok");

        }


        //collectionviewOpenRequests.ItemsSource = myOrders;

        //activityIndicator.IsRunning = false;

    }

    private void toolbaritemRefresh_Clicked(object sender, EventArgs e)
    {
        RefreshForm();
    }


    */

    private void btnNextStep_Clicked(object sender, EventArgs e)
    {
        if (GlobalClass.m_UserId == "" || GlobalClass.m_UserId == null)
        {
            DisplayAlert("هشدار", "به عنوان کاربر معتبر وارد برنامه نشده ای", "ok");
            return;
        }

        CustomerServiceItemViewModel item2 = (CustomerServiceItemViewModel)(listviewServiceItemType.SelectedItem);
        if (item2 == null)
        {
            DisplayAlert("", "یک سرویس رو انتخاب کن", "ok");
            return;
        }

        //GlobalClass.m_ServiceType = item2.ServiceType;
        //GlobalClass.m_ServiceItemType = item2.ServiceItemType;
        //GlobalClass.m_ServiceStatus = item2.ServiceStatus;
        //GlobalClass.m_ServiceItemStatus = item2.ServiceItemStatus;
        GlobalClass.m_CustomerServiceItemView = item2;

        //TabbedPage aa = (TabbedPage)this.Parent;
        //aa.CurrentPage = aa.Children[1];

        ///روش سنتی
        //Navigation.PushAsync(new MapPage());

        ///روش جدید برپایه 
        ///AppShell
        ///
        switch (GlobalClass.m_CustomerServiceItemView.ServiceType)
        {
            case (byte)GlobalClass.enumServiceType.servicetype_NoSelect:
          
                break;
            case (byte)GlobalClass.enumServiceType.servicetype_Part:
                Shell.Current.GoToAsync(nameof(MapPage));
                break;
            case (byte)GlobalClass.enumServiceType.servicetype_OnLocationService:
                DisplayAlert("آگاهی", "این خدمت هنوز ارائه نشده است", "ok");
                break;
            case (byte)GlobalClass.enumServiceType.servicetype_OnSiteService:
                DisplayAlert("آگاهی", "این خدمت هنوز ارائه نشده است", "ok");
                break;
            case (byte)GlobalClass.enumServiceType.servicetype_CarTowing:
                DisplayAlert("آگاهی", "این خدمت هنوز ارائه نشده است", "ok");
                break;
        }
        

    }


    private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (rdoPart.IsChecked)
        {

            listviewServiceItemType.ItemsSource = mySaleServices;
        }

        if (rdoLocationService.IsChecked)
        {
            listviewServiceItemType.ItemsSource = myLocationServices;
        }
        if (rdoSiteService.IsChecked)
        {
            listviewServiceItemType.ItemsSource = mySiteServices;
        }
        if (rdoTowing.IsChecked)
        {
            listviewServiceItemType.ItemsSource = myTowingServices;
        }
    }

    private void listviewServiceItemType_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        CustomerServiceItemViewModel item2 = (CustomerServiceItemViewModel)(listviewServiceItemType.SelectedItem);
        if (item2 != null)
        {
            GlobalClass.m_CustomerServiceItemView = item2;
        }

       
        
    }
}