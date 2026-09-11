using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using oto1;
using oto1.Models;
using oto1.Services;
using static System.Net.Mime.MediaTypeNames;

namespace oto1;

public partial class SignInPage : ContentPage
{
    public ServiceController MyServiceController { get; set; }



    public SignInPage()
    {
        InitializeComponent();
    }

    private async void btnSignIn_Clicked(object sender, EventArgs e)
    {
        AppUserModel myUser = new AppUserModel();
        string username = txtUsername.Text;
        string password = txtPassword.Text;

        // اینجا میتوانید کد احراز هویت خود را اضافه کنید
        myactivityIndicator.IsRunning = true;
        MyServiceController = new ServiceController();
        myUser = await MyServiceController.AuthenticateUser(username, password);

        if (myUser != null)
        {
            var toast = Toast.Make("هویت شما شناسایی شد", CommunityToolkit.Maui.Core.ToastDuration.Long, 12);

            await toast.Show();

            await AuthorizeUser(username);

            if (GlobalClass.m_VendorId != 0)
            {
                toast = Toast.Make("دسترسی شما شناسایی شد", CommunityToolkit.Maui.Core.ToastDuration.Long, 12);
                await toast.Show();
                WeakReferenceMessenger.Default.Send(new AppShell.UserToolbarMessage("updateUserToolbarItem"));

                myactivityIndicator.IsRunning = false;
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "به این برنامه دسترسی نداری", "OK");
                myactivityIndicator.IsRunning = false;
            }
        }
        else
        {
            GlobalClass.m_UserId = "";
            GlobalClass.m_VendorId = 0;
            await DisplayAlert("Error", "شناسه یا گذرواژه نادرست است", "OK");
            myactivityIndicator.IsRunning = false;

        }

    }


    private async Task<VendorModel> AuthorizeUser(string username)
    {

        MyServiceController = new ServiceController();
        VendorModel myvendor = new VendorModel();

        try
        {
            myvendor = await MyServiceController.GetVendorByUserId(username);

            GlobalClass.m_UserId = username;
            GlobalClass.m_VendorName = myvendor.FName;
            GlobalClass.m_VendorId = myvendor.VendorId;

        }
        catch (Exception ex)
        {
            GlobalClass.m_UserId = "";
            GlobalClass.m_VendorId = 0;
        }


        return await Task.FromResult(myvendor);
    }



    private void OnSignUpTapped(object sender, TappedEventArgs e)
    {
        //await Launcher.OpenAsync("https://www.androidmads.info/search/label/.net maui");
        Navigation.PushAsync(new SignUp());
    }
}
  