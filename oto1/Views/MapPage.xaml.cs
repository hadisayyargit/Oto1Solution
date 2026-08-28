using Microsoft.Maui.Maps;
//using Map = Microsoft.Maui.Controls.Maps.Map;
using Microsoft.Maui.Controls.Maps;
using oto1.Services;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Diagnostics;
using oto1.Models;

namespace oto1;

public partial class MapPage : ContentPage
{
    public ServiceController MyServiceController { get; set; }

    List<LocationAddressModel> myLocationList = new List<LocationAddressModel>();

    public long? LocationAddressId { get; set; }



    public MapPage()
    {
        InitializeComponent();
        this.MyServiceController = new ServiceController();

        InitmyMap();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        FillLocationCollection();
    }

    private void ImageButtonRefreshMap_Clicked(object sender, EventArgs e)
    {

        ///Move To Location
        goToMap(35.6891975, 51.3889736);


        /*

        /// Zoom
        double zoomLevel =2.0;
        double latlongDegrees = 360 / (Math.Pow(2, zoomLevel));
        if (mymap.VisibleRegion != null)
        {
            mymap.MoveToRegion(new MapSpan(mymap.VisibleRegion.Center, latlongDegrees, latlongDegrees));
        }
        */



        /*
         map.PinDragStart += (_, e) => labelDragStatus.Text = $"DragStart - {PrintPin(e.Pin)}";
map.PinDragging += (_, e) => labelDragStatus.Text = $"Dragging - {PrintPin(e.Pin)}";
map.PinDragEnd += (_, e) => labelDragStatus.Text = $"DragEnd - {PrintPin(e.Pin)}";
         
         */


        Content = stack1;
    }

    void goToMap(double Latitude, double Longitude)
    {
        Location location = new Location(Latitude, Longitude);
        MapSpan mapSpan = new MapSpan(location, 0.01, 0.01);
        mymap.MoveToRegion(mapSpan);
    }

 


private async void InitmyMap()
    {
#if ANDROID || IOS
        await Platform.WaitForActivityAsync();
#endif

        //if (DeviceInfo.Platform == DevicePlatform.Android)
        //{
        //    await Platform.WaitForActivityAsync();
        //}

        mymap.IsShowingUser = true;
        mymap.MapType = MapType.Street;

        GlobalClass.m_MapSelected = false;

        if (NetClass.CheckNetConnection() == false)
        {
            await DisplayAlert("پیام", "internet connection fail", "ok");
        }


        ///رفتن به موقعیت GPS
        var geoLocationRequest = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));
        try
        {
            var location = await Geolocation.GetLocationAsync(geoLocationRequest);
            mymap.MoveToRegion(MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(10)));

        }
        catch (Exception ex)
        {
            ///رفتن به موقعیت انتخابی
            Location location1 = new Location(35.6891975, 51.3889736);
            goToMap(location1.Latitude, location1.Longitude);

        }



        /// checking for GPS method2
        try
        {
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(2));
            var _gpsCheckCts = new CancellationTokenSource();
            Location location2 = await Geolocation.Default.GetLocationAsync(request, _gpsCheckCts.Token);
        }
        catch (FeatureNotEnabledException)
        {
            await DisplayAlert("پیام", "GPS خاموشه", "ok");
            Debug.WriteLine("GPS is not enabled. Please turn on GPS.");
        }


        FillLocationCollection();
    }





    private void mymap_MapClicked(object sender, MapClickedEventArgs e)
    {

        //if(mymap.IsZoomEnabled=2)
        //DisplayAlert("پیام", e.Location.Latitude.ToString(), "ok");
        Pin pin = new Pin
        {
            Label = "label",
            Address = "The city with a boardwalk",
            Type = PinType.Place,
            Location = new Location(e.Location.Latitude, e.Location.Longitude)

        };

        
        mymap.Pins.Clear();
        mymap.Pins.Add(pin);


        GlobalClass.m_Latitude = Convert.ToDecimal( e.Location.Latitude);
        GlobalClass.m_Longitude = Convert.ToDecimal(e.Location.Longitude);
        txtPName.Text = "";
    }




    private async void btnAddLocation_Clicked(object sender, EventArgs e)
    {
        if (txtPName.Text == null || txtPName.Text == "")
        {
            await DisplayAlert("خطا", "نام نشانی را وارد کنید", "ok");
            return;
        }

        if (NetClass.CheckNetConnection() == false)
        {
            await DisplayAlert("پیام", "Internet Connection Fail", "ok");
        }


        LocationAddressModel loc1 = new LocationAddressModel();
        LocationAddressModel newLoc = new LocationAddressModel();


        /*  update test
        loc1.Id = 1;
        loc1.FName = "تست شماره۲";

        try
        {
           
        }
        catch (Exception ex)
        {
            await DisplayAlert("", ex.Message, "ok");
        }
        */
        try
        {

            loc1.FName = txtPName.Text;
            loc1.CustomerId = GlobalClass.m_CustomerId;
            loc1.Latitude = GlobalClass.m_Latitude;
            loc1.Longitude = GlobalClass.m_Longitude;
            loc1.Address = txtAddress.Text;

            //if (this.LocationAddressId > 0)
            //{
            //    loc1.LocationAddressId = Convert.ToInt64(this.LocationAddressId);
            //    newLoc = await this.MyServiceController.UpdateLocationAddress(loc1);

            //}
            //else
            //{
            //    newLoc = await this.MyServiceController.AddLocationAddress(loc1);
            //}

            newLoc = await this.MyServiceController.AddLocationAddress(loc1);

            if (newLoc != null)
            {
                await DisplayAlert("پیام", "نشانی ذخیره شد", "ok");
                //await DisplayAlert("", newLoc.Id.ToString(), "ok");

                FillLocationCollection();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("", ex.Message, "ok");
        }

    }


    private async void FillLocationCollection()
    {

        if (NetClass.CheckNetConnection() == false)
        {
            await DisplayAlert("پیام", "Internet Connection Fail", "ok");
        }
        else
        {
            myLocationList = await MyServiceController.GetAllLocationAddressess(GlobalClass.m_CustomerId, 0, true);
            collectionviewLocations.ItemsSource = myLocationList;
        }


        if (GlobalClass.m_MapSelected)
        {

           // txtLongitude.Text = GlobalClass.Longitude.ToString();
          //  txtLatitude.Text = GlobalClass.Latitude.ToString();
        }
    }

    private async void SwipeItem_Clicked(object sender, EventArgs e)
    {
        SwipeItem item1 = (SwipeItem)sender;


        var Loc1 = (LocationAddressModel)item1.BindingContext;


        try
        {
            bool res = await this.MyServiceController.DeleteLocationAddress(Loc1.LocationAddressId);
            if (res)
            {
                await DisplayAlert("", "نشانی پاک شد", "ok");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("", ex.Message, "ok");
        }

        FillLocationCollection();
    }

    private void collectionviewLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        LocationAddressModel loc = (LocationAddressModel)e.CurrentSelection.FirstOrDefault();
        if (loc == null) return;
        //DisplayAlert("", loc.Id.ToString(), "ok");




        this.LocationAddressId = loc.LocationAddressId;
        //GlobalClass.m_LocationAddressId = loc.LocationAddressId;

        txtPName.Text = loc.FName;
        txtAddress.Text = loc.Address;
        GlobalClass.m_Latitude = loc.Latitude;
        GlobalClass.m_Longitude = loc.Longitude;

        // ((CollectionView)sender).SelectedItem = null;
        double latitude1 = Convert.ToDouble( GlobalClass.m_Latitude)
           , longitude1 = Convert.ToDouble(GlobalClass.m_Longitude);


        

        Pin pin = new Pin
        {
            Label = "label",
            Address = "The city with a boardwalk",
            Type = PinType.Place,
            Location = new Location(latitude1, longitude1)

        };


        mymap.Pins.Clear();
        mymap.Pins.Add(pin);

        goToMap(latitude1, longitude1);

    }


    private void btnNextStep_Clicked(object sender, EventArgs e)
    {
        GlobalClass.m_Address = txtAddress.Text;

        //var m2 = Shell.Current.CurrentItem.CurrentItem.CurrentItem.Route;

            //Navigation.PushAsync(new Lubricant());
            //Shell.Current.GoToAsync("//Lubricant");
     

        if (GlobalClass.m_UserId == "" || GlobalClass.m_UserId == null)
        {
            DisplayAlert("هشدار", "به عنوان کاربر معتبر وارد برنامه نشده‌ای", "ok");
            return;
        }

        /*
        if (GlobalClass.m_LocationAddressId == 0 )
        {
            DisplayAlert("هشدار", "جایگاه (موقعیت مکانی) را انتخاب نکردی!", "ok");
            return;

        }
        */
        

        if (GlobalClass.m_CustomerServiceItemView == null)
        {
            DisplayAlert("هشدار", "نوع خدمت را انتخاب نکرده‌ای", "ok");
            return;

        }

        switch (GlobalClass.m_CustomerServiceItemView.ServiceType)
        {
            case (byte)GlobalClass.enumServiceType.servicetype_Part:
                if (GlobalClass.m_CustomerServiceItemView.tmp_Flag == "lubricant")
                    Navigation.PushAsync(new Lubricant());
                else
                    DisplayAlert("آگاهی", "این خدمت هنوز ارائه نشده است", "ok");
                break;
        }

       
    }

    private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {

        var searchBar = (SearchBar)sender;
        string query = searchBar.Text;

        /*
        try
        {
            var locations = await Geocoding.GetLocationsAsync(query);
            var location = locations?.FirstOrDefault();
        }
        catch(Exception ex) 
        {
            await DisplayAlert("", ex.Message, "ok");
        }
        */

        try
        {
            Location mylocation = new Location();

           // var locations = await Geocoding.GetLocationsAsync(query);
           // mylocation = locations?.FirstOrDefault();

             mylocation = await Geolocation.Default.GetLastKnownLocationAsync();

            if (mylocation != null)
            {
                await DisplayAlert("", $"Latitude: {mylocation.Latitude}, Longitude: {mylocation.Longitude}, Altitude: {mylocation.Altitude}", "ok");

                goToMap(mylocation.Latitude, mylocation.Longitude);

            }
        }
     
        catch (Exception ex)
        {
            // Unable to get location
        }




    }

    private async void btnBeforeStep_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();

    }
}