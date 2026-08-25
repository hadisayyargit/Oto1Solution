namespace oto1;

using oto1.Services;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Diagnostics;
using oto1.Models;
public partial class myLocationAddresses : ContentPage
{
    public ServiceController MyServiceController { get; set; }

    //public double Latitude { get; set; }
    //public double Longitude { get; set; }
    List<LocationAddressModel> myLocationList = new List<LocationAddressModel>();

    public long? LocationAddressId { get; set; }
    public myLocationAddresses()
	{
		InitializeComponent();
        this.MyServiceController = new ServiceController();
        //RefreshForm();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshForm();
    }
    private void btnMap_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new MapPage());
    }

    private async void RefreshForm()
    {

        if (NetClass.CheckNetConnection() == false)
        {
            await DisplayAlert("پیام", "Internet Connection Fail", "ok");
        }
        else
        {
            myLocationList = await MyServiceController.GetAllLocationAddressess(0, 0, true);
            collectionviewLocations.ItemsSource = myLocationList;
        }

    }

    private void collectionviewLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void SwipeItem_Clicked(object sender, EventArgs e)
    {

    }
}