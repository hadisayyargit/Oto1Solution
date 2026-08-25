using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oto1.Services
{
    public partial class NetClass
    {
        public static bool CheckNetConnection()
        {
            bool res;

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                res = false;


            }
            else
            {
                res = true;
                //DisplayAlert("پیام", "درود", "ok");
            }


            return res;
        }

    public static async Task<bool> CheckNetFileExists(string url)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                // ارسال یک درخواست HEAD به URL
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, url);
                HttpResponseMessage response = await client.SendAsync(request);

                // بررسی کد وضعیت پاسخ
                return response.IsSuccessStatusCode;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"خطا: {ex.Message}");
            return false;
        }
    }
    


    /// checking for GPS method1
    //if (DeviceInfo.Platform == DevicePlatform.Android)
    //{
    //    var lm = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.GetSystemService(Android.Content.Context.LocationService) as Android.Locations.LocationManager;
    //    var isenabled = lm.IsProviderEnabled(Android.Locations.LocationManager.GpsProvider);
    //    // check if GPS is enabled
    //    if (isenabled == false)
    //    {
    //        //Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.StartActivity(new Android.Content.Intent(Android.Provider.Settings.ActionLocationSourceSettings));
    //        //go to the android location settings page
    //    }
    //}
    ///----------------------------------
    ///

    /*
        /// checking for GPS method2
    try
    {
        GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(2));
    var _gpsCheckCts = new CancellationTokenSource();
    Location location2 = await Geolocation.Default.GetLocationAsync(request, _gpsCheckCts.Token);
}
    catch (FeatureNotEnabledException)
    {
        await DisplayAlert("پیام", "GPS is Disabled", "ok");
Debug.WriteLine("GPS is not enabled. Please turn on GPS.");
    }
    */
}
}
