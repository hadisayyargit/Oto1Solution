//using Map = Microsoft.Maui.Controls.Maps.Map;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Maps;
using oto1.Models;
using oto1.Services;
using System.Diagnostics;
using System.Threading;

namespace oto1;


public partial class MapPage : ContentPage
{
    public ServiceController MyServiceController { get; set; }

    List<LocationAddressModel> myLocationList = new List<LocationAddressModel>();
    List<CityModel> filteredcities = new List<CityModel>();
    public long? LocationAddressId { get; set; }



    public MapPage()
    {
        InitializeComponent();
        this.MyServiceController = new ServiceController();

        InitmyMap();

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        FillLocationCollection();
        await LoadProvinces();
    }

    private void ImageButtonRefreshMap_Clicked(object sender, EventArgs e)
    {

        goToMap(35.6891975, 51.3889736);

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

        mymap.IsShowingUser = true;
        mymap.MapType = MapType.Street;

        GlobalClass.m_MapSelected = false;

        txtPName.Text = GlobalClass.m_VendorName;

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

        Pin pin = new Pin
        {
            Label = "label",
            Address = "The city with a boardwalk",
            Type = PinType.Place,
            Location = new Location(e.Location.Latitude, e.Location.Longitude)

        };


        mymap.Pins.Clear();
        mymap.Pins.Add(pin);


        GlobalClass.m_Latitude = Convert.ToDecimal(e.Location.Latitude);
        GlobalClass.m_Longitude = Convert.ToDecimal(e.Location.Longitude);
        txtPName.Text = "";
    }

    private async void btnAddLocation_Clicked(object sender, EventArgs e)
    {

        if (txtPName.Text == null || txtPName.Text == "")
        {
            await DisplayAlert("خطا", "عنوان یا نشانی را وارد کن", "ok");
            return;
        }

        if (pickerCity.SelectedIndex==-1)
        {
            await DisplayAlert("خطا", "شهر را وارد کن", "ok");
            return;
        }

        int cityid = ((CityModel)pickerCity.ItemsSource[pickerCity.SelectedIndex]).CityId;

        if (NetClass.CheckNetConnection() == false)
        {
            await DisplayAlert("پیام", "Internet Connection Fail", "ok");
        }

        LocationAddressModel loc1 = new LocationAddressModel();
        LocationAddressModel newLoc = new LocationAddressModel();

        try
        {

            loc1.PName = txtPName.Text;
            loc1.VendorId = GlobalClass.m_VendorId;
            loc1.Latitude = GlobalClass.m_Latitude;
            loc1.Longitude = GlobalClass.m_Longitude;
            loc1.Address = txtAddress.Text;
            loc1.CityId = cityid;

            newLoc = await this.MyServiceController.AddLocationAddress(loc1);

            if (newLoc != null)
            {
                await DisplayAlert("پیام", "نشانی ذخیره شد", "ok");

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
            myLocationList = await MyServiceController.GetAllLocationAddressess( -1, GlobalClass.m_VendorId,-1);
            collectionviewLocations.ItemsSource = myLocationList;
        }


        if (GlobalClass.m_MapSelected)
        {

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

    private async void collectionviewLocations_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        LocationAddressModel loc = (LocationAddressModel)e.CurrentSelection.FirstOrDefault();
        if (loc == null) return;

        if (loc.CityId != null)
        {
            // ۱. دریافت استان از سرویس
            ProvinceModel myprovince = await MyServiceController.GetProvinceOfCity(loc.CityId.Value);

            //pickerProvince.SelectedIndex = 4;

            if (myprovince != null)
            {
                int provinceIndex = GlobalClass.m_AllProvinces.FindIndex(p => p.ProvinceId == myprovince.ProvinceId);

                if (provinceIndex != -1)
                {
                    pickerProvince.SelectedIndex = provinceIndex;

                    var cities = await MyServiceController.GetCitiesOfProvince(myprovince.ProvinceId);
                    pickerCity.ItemsSource = cities;

                    CityModel city = cities.FirstOrDefault(c => c.CityId == loc.CityId.Value);
                    if (city != null)
                    {
                        pickerCity.SelectedItem = city;
                    }
                }
            }

        }

        this.LocationAddressId = loc.LocationAddressId;
        txtPName.Text = loc.PName;
        txtAddress.Text = loc.Address;
        GlobalClass.m_Latitude = loc.Latitude;
        GlobalClass.m_Longitude = loc.Longitude;

        double latitude1 = Convert.ToDouble(GlobalClass.m_Latitude);
        double longitude1 = Convert.ToDouble(GlobalClass.m_Longitude);

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



    private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        var searchBar = (SearchBar)sender;
        string query = searchBar.Text;

        if (string.IsNullOrWhiteSpace(query))
            return;

        try
        {
            // ۱. تلاش برای پیدا کردن مختصات از روی متن (Geocoding)
            var locations = await Geocoding.GetLocationsAsync(query);
            // mylocation = await Geolocation.Default.GetLastKnownLocationAsync();


            var foundLocation = locations?.FirstOrDefault();

            if (foundLocation != null)
            {

                goToMap(foundLocation.Latitude, foundLocation.Longitude);

                mymap.Pins.Clear();

                Pin searchPin = new Pin
                {
                    Label = "محل جستجو شده",
                    Address = query,
                    Type = PinType.Place,
                    Location = foundLocation
                };

                mymap.Pins.Add(searchPin);

                // پاک کردن متن جستجو برای جستجوی بعدی
                searchBar.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("خطا", "مکان مورد نظر پیدا نشد. لطف‌تر نام یا آدرس را وارد کن.", "باشه");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("خطای سیستم", "خطایی در جستجو رخ داد. لطفا اتصال اینترنت خود را بررسی کن.", "باشه");
            System.Diagnostics.Debug.WriteLine($"Search Error: {ex.Message}");
        }
    }


    private async void btnBeforeStep_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();

    }

    private async void btnSetCurrentAddress_Clicked(object sender, EventArgs e)
    {
        GlobalClass.m_Address = txtAddress.Text;
        //collectionviewLocations


        if (collectionviewLocations.SelectedItem == null)
        {
            await DisplayAlert("خطا", "نشانی را انتخاب کن", "ok");
            return;
        }
        
        if (pickerCity.SelectedIndex == -1)
        {
            await DisplayAlert("خطا", "شهر را وارد کن", "ok");
            return;
        }
        var selectedlocation = collectionviewLocations.SelectedItem as LocationAddressModel;

        if (selectedlocation != null)
        {

            var res = MyServiceController.SetCurrentAddress(selectedlocation.LocationAddressId);

            await Task.Delay(1000);

            if (res != null)
            {
                await DisplayAlert("پیام", "نشانی فعلی ذخیره شد", "ok");
            }
        }
        else
        {
            await DisplayAlert("پیام", "نشانی انتخاب نکردی", "ok");
        }
    }
    private async Task LoadProvinces()
    {
        GlobalClass.m_AllProvinces = await MyServiceController.GetAllProvince();
        GlobalClass.m_AllCities = await MyServiceController.GetAllCity();
        pickerProvince.ItemsSource = GlobalClass.m_AllProvinces;
        pickerCity.ItemsSource = new List<CityModel>(); // در ابتدا خالی باشد
    }


    private async void pickerProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedProvince = pickerProvince.SelectedItem as ProvinceModel;



        filteredcities = await MyServiceController.GetCitiesOfProvince(selectedProvince.ProvinceId);
        pickerCity.ItemsSource = filteredcities;
        pickerCity.SelectedIndex = -1; // ریست کردن شهر
        txtAddress.Text = "";
        txtPName.Text = "";
    }

    private void pickerCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedCity = pickerCity.SelectedItem as CityModel;
        if (selectedCity != null)
        {
            GlobalClass.m_CityId = selectedCity.CityId;
            goToMap(decimal.ToDouble(selectedCity.Latitude.Value), decimal.ToDouble(selectedCity.Longitude.Value));

            txtAddress.Text = "";
            txtPName.Text = "";
        }
    }
}