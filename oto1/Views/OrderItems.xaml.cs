using oto1.Models;
using oto1.Services;

namespace oto1;

public partial class OrderItems : ContentPage
{
    public long ServiceId { get; set; }
    public CustomerServiceController myCustomerServiceController { get; set; }

    public OrderItems()
	{
		InitializeComponent();
	}
    public OrderItems(long serviceid)
    {
        InitializeComponent();
        this.ServiceId = serviceid;
       // labelServiceId.BindingContext = this;

        RefreshForm();
    }
    private async void RefreshForm()
    {
        this.myCustomerServiceController = new CustomerServiceController();


        List<CustomerServiceItemViewModel> myOrdersItems = new List<CustomerServiceItemViewModel>();
        myOrdersItems = await myCustomerServiceController.GetCustomerServiceItem(this.ServiceId);
        foreach (var item in myOrdersItems)
        {
            item.ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/part/prt" + item.PartId.ToString() + ".png";
        }
        collectionviewOrderItems.ItemsSource = myOrdersItems;
    }

    }