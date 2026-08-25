namespace oto1;
using oto1.Services;
using oto1.Models;
public partial class HomePage : ContentPage
{
    public ServiceController MyServiceController { get; set; }
    //public List<ProductModel> SlideList;

    public HomePage()
    {
        InitializeComponent();
        this.MyServiceController = new ServiceController();

    }


    private async void btnMyRequests_Clicked(object sender, EventArgs e)
    {

        ((AppShell)App.Current.MainPage).SwitchtoTab("tabRequest");
    }


}