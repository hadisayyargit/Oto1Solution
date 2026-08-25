using oto1;
using oto1.Models;
using oto1.Services;

namespace oto1;

public partial class Basket : ContentPage
{
    //public ServiceController MyServiceController { get; set; }

    public CustomerServiceController myCustomerServiceController { get; set; }

    public Basket()
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

        long serviceid = -1;
        long customerid = -1;
        int postmanid = -1;
        int vendorid = GlobalClass.m_VendorId;
        string servicestatusserie = ((byte)GlobalClass.enumServiceStatus.servicestatus_Payment).ToString()
            + "," + ((byte)GlobalClass.enumServiceStatus.servicestatus_Pending).ToString();

        string ticketdatetimeBegin = "2024-01-01";
        string ticketdatetimeEnd = DateTime.Today.ToString("yyyy-MM-dd");

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
            /// voice
            await TextToSpeech.SpeakAsync("Oto1");


            foreach (var req in myOrders)
            {
                
                List<CustomerServiceItemViewModel> myOrdersItems = new List<CustomerServiceItemViewModel>();
                myOrdersItems = await myCustomerServiceController.GetCustomerServiceItem(req.ServiceId);
                foreach (var item in myOrdersItems)
                {
                    item.ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/part/prt" + item.PartId.ToString() + ".png";
                }

                myGroupServices.Add(new CustomerServiceGroupModel(req.ServiceId, req.TicketDateTime, req.VendorName,req.CustomerName,req.Address, req.ServiceTypeName, req.ServiceStatusName, req.ItemCount, req.TotalAmount,req.CustomerThumbnailPhotoFile , myOrdersItems));


            }
        }

        collectionviewOrders.ItemsSource = myGroupServices;

        activityIndicator.IsRunning = false;
        activityIndicator.IsVisible = false;
    }



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


        Navigation.PushAsync(new BasketItems(id));
    }



}