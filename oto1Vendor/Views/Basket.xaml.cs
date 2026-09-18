using oto1;
using oto1.Models;
using oto1.Services;

namespace oto1;

public partial class Basket : ContentPage
{
    //public ServiceController MyServiceController { get; set; }

    public CustomerServiceController myCustomerServiceController { get; set; }
    public byte ServiceStatus { get; set; }

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
    {        ///<hadi>
             /// خط پایین حتما باید باشد وگرنه 
             /// tab
             /// قبلی رو در نظر میگیره
             /// </hadi>
        await Task.Delay(50);

        var currentRoute = Shell.Current.CurrentState.Location.ToString();


        if (currentRoute.Contains("payment"))
            this.ServiceStatus = (byte)GlobalClass.enumServiceStatus.servicestatus_Payment;
        else if (currentRoute.Contains("sending"))
            this.ServiceStatus = (byte)GlobalClass.enumServiceStatus.servicestatus_Sending;
        else if (currentRoute.Contains("refused"))
            this.ServiceStatus = (byte)GlobalClass.enumServiceStatus.servicestatus_Refusal;
        else if (currentRoute.Contains("rejected"))
            this.ServiceStatus = (byte)GlobalClass.enumServiceStatus.servicestatus_Reject;
        else if (currentRoute.Contains("done"))
            this.ServiceStatus = (byte)GlobalClass.enumServiceStatus.servicestatus_Done;

        activityIndicator.IsRunning = true;
        activityIndicator.IsVisible = true;

        this.myCustomerServiceController = new CustomerServiceController();

        List<CustomerServiceViewModel> myOrders = new List<CustomerServiceViewModel>();
        List<CustomerServiceGroupModel> myGroupServices = new List<CustomerServiceGroupModel>();

        long serviceid = -1;
        long customerid = -1;
        int postmanid = -1;
        int vendorid = GlobalClass.m_VendorId;
        // string servicestatusserie = ((byte)GlobalClass.enumServiceStatus.servicestatus_Payment).ToString()
        //    + "," + ((byte)GlobalClass.enumServiceStatus.servicestatus_Pending).ToString();

       
        string servicestatusserie = this.ServiceStatus.ToString();
        if (this.ServiceStatus == (byte)GlobalClass.enumServiceStatus.servicestatus_Payment)
            servicestatusserie = ((byte)GlobalClass.enumServiceStatus.servicestatus_Payment).ToString()
            + "," + ((byte)GlobalClass.enumServiceStatus.servicestatus_Pending).ToString();


        string ticketdatetimeBegin = DateTime.Today.ToString("yyyy-MM-dd");
        string ticketdatetimeEnd   = DateTime.Today.ToString("yyyy-MM-dd");

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
            if (this.ServiceStatus == (byte)GlobalClass.enumServiceStatus.servicestatus_Payment)
            {
                /// voice
                await TextToSpeech.SpeakAsync("Oto1");
            }

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