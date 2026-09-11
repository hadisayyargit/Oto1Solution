using oto1;
using oto1.Models;
using oto1.Services;

namespace oto1;

public partial class Orders : ContentPage
{
    //public ServiceController MyServiceController { get; set; }

    public CustomerServiceController myCustomerServiceController { get; set; }

    public Orders()
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
        if (GlobalClass.m_UserId == "" || GlobalClass.m_UserId == null)
        {
            await DisplayAlert("هشدار", "به عنوان کاربر معتبر وارد برنامه نشده‌ای", "ok");
            return;
        }

        activityIndicator.IsRunning = true;
        activityIndicator.IsVisible = true;

        this.myCustomerServiceController = new CustomerServiceController();

        List<CustomerServiceViewModel> myOrders = new List<CustomerServiceViewModel>();
        List<CustomerServiceGroupModel> myGroupServices = new List<CustomerServiceGroupModel>();

        long serviceid = -1;
        long customerid = -1;
        int postmanid = -1;
        int vendorid = -1;

        string ticketdatetimeBegin = DateTime.Today.ToString("yyyy-MM-dd");
        string ticketdatetimeEnd = DateTime.Today.ToString("yyyy-MM-dd");


        var d1 = dtPickerBegin.SelectedPersianDate;
        var d2 = dtPickerEnd.SelectedPersianDate;

        if (d1 != null)
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

            string[] ss = d1.ToString().Split("/");
            ticketdatetimeBegin = pc.ToDateTime(int.Parse(ss[0]), int.Parse(ss[1]), int.Parse(ss[2]), 0, 0, 0, 0).ToString("yyyy-MM-dd");
        }
        if (d2 != null)
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

            string[] ss = d2.ToString().Split("/");
            ticketdatetimeEnd = pc.ToDateTime(int.Parse(ss[0]), int.Parse(ss[1]), int.Parse(ss[2]), 0, 0, 0, 0).ToString("yyyy-MM-dd");
        }


        string servicestatusserie = ((byte)GlobalClass.enumServiceStatus.servicestatus_NoSelect).ToString();

        try
        {
            myOrders = await myCustomerServiceController.GetCustomerService(serviceid, customerid, vendorid,postmanid, ticketdatetimeBegin, ticketdatetimeEnd, servicestatusserie);

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

                myGroupServices.Add(new CustomerServiceGroupModel(req.ServiceId, req.TicketDateTime, req.VendorName, req.CustomerName, req.Address, req.ServiceTypeName, req.ServiceStatusName, req.ItemCount, req.TotalAmount, req.CustomerThumbnailPhotoFile, myOrdersItems));


            }
        }



        collectionviewOrders.ItemsSource = myGroupServices;


        activityIndicator.IsRunning = false;
        activityIndicator.IsVisible = false;
    }

    /*
    private async void RefreshForm1()
    {
        activityIndicator.IsRunning = true;

        // this.MyServiceController = new ServiceController();



        this.myCustomerServiceController = new CustomerServiceController();

        List<CustomerServiceViewModel> myOrders = new List<CustomerServiceViewModel>();
        long serviceid = -1, customerid = GlobalClass.m_CustomerId;
        int vendorid = -1; byte servicestatus = 255;
        string ticketdatetimeBegin = DateTime.Today.ToString("yyyy-MM-dd");
        string ticketdatetimeEnd = DateTime.Today.ToString("yyyy-MM-dd");

        var d1 = dtPickerBegin.SelectedPersianDate;
        var d2 = dtPickerEnd.SelectedPersianDate;

        if (d1 != null)
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

            string[] ss = d1.ToString().Split("/");
            ticketdatetimeBegin = pc.ToDateTime(int.Parse(ss[0]), int.Parse(ss[1]), int.Parse(ss[2]), 0, 0, 0, 0).ToString("yyyy-MM-dd");
        }
        if (d2 != null)
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

            string[] ss = d2.ToString().Split("/");
            ticketdatetimeEnd = pc.ToDateTime(int.Parse(ss[0]), int.Parse(ss[1]), int.Parse(ss[2]), 0, 0, 0, 0).ToString("yyyy-MM-dd");
        }



        try
        {
            myOrders = await myCustomerServiceController.GetCustomerService(serviceid, customerid, vendorid, ticketdatetimeBegin, ticketdatetimeEnd, servicestatus);

        }
        catch (Exception ex)
        {
            await DisplayAlert("پیام", ex.Message, "ok");

        }


        collectionviewOrders.ItemsSource = myOrders;

          activityIndicator.IsRunning = false;

    }
    */

    private void toolbaritemRefresh_Clicked(object sender, EventArgs e)
    {

        RefreshForm();
    }

    private void btnServiceItems_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var servicesitems = (CustomerServiceGroupModel)button.BindingContext;

        var myCustomerServiceGroup = servicesitems.ToList().FirstOrDefault();
        long id = servicesitems.ServiceId;


        Navigation.PushAsync(new OrderItems(id));
    }


    private void mnuAllCars_Clicked(object sender, EventArgs e)
    {
        //Navigation.PushAsync(new AllCars());

    }



}