using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

using oto1.Services;

namespace oto1;

public partial class AppShell : Shell
{
    // تعریف کلاس پیام (درون AppShell یا فایل جداگانه)
    public class UserToolbarMessage : ValueChangedMessage<string>
    {
        public UserToolbarMessage(string value) : base(value) { }
    }

    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
        Routing.RegisterRoute(nameof(Requests), typeof(Requests));

   



        // ثبت‌نام برای دریافت پیام (داخل سازنده)
        WeakReferenceMessenger.Default.Register<UserToolbarMessage>(this, (r, m) =>
        {
            // از MainThread استفاده می‌کنیم تا UI حتماً به‌روز شود
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (m.Value == "updateUserToolbarItem")
                {
                    updateUserToolbarItem(m.Value);
                }
            });
        });

    }

    private async void sayhello()
    {
        ServiceController MyServiceController = new ServiceController();
        string s = "";

        //s = await MyServiceController.SayHello();

        //CustomerModel mycustomer = await MyServiceController.GetCustomerByUserId("madjid", "test");
    }

    private void UpdateToolbarItem(string s)
    {

        toolbaritemBasket.Text = s;
    }

    private async void updateUserToolbarItem(string s)
    {
        toolbaritemUser.IconImageSource = "user5.png";

        if (GlobalClass.m_UserId != "")
        {
            string fileUrl = "https://khordadnet.ir/mysites/oto1/assets/customer/c" + GlobalClass.m_CustomerId.ToString() + ".png";

            bool fileExists = await NetClass.CheckNetFileExists(fileUrl);
            if (fileExists) toolbaritemUser.IconImageSource = fileUrl;
        }


    }
    private void toolbaritemBasket_Clicked(object sender, EventArgs e)
    {
        //Navigation.PushAsync(new Baskets());
        if (GlobalClass.m_UserId == "" || GlobalClass.m_UserId == null)
        {
            DisplayAlert("هشدار", "به عنوان کاربر معتبر وارد برنامه نشده ای", "ok");
            return;
        }
        // Shell.Current.GoToAsync(nameof(Baskets));
        Navigation.PushAsync(new Baskets());
    }

    private async void toolbaritemUser_Clicked(object sender, EventArgs e)
    {
        if (GlobalClass.m_UserId == "" || GlobalClass.m_UserId == null)
        {
            Navigation.PushAsync(new SignInPage());
          // await Shell.Current.GoToAsync(nameof(SignInPage));

        }
        else
        {
            bool answer = await DisplayAlert("هشدار", "از حساب خود خارج می‌شوی؟", "آره", "نه");

            if (answer == true)
            {
                toolbaritemLogoff_Clicked(this, null);
            }
        }
    }


    private void toolbaritemLogoff_Clicked(object sender, EventArgs e)
    {
        GlobalClass.m_UserId = "";
        toolbaritemUser.IconImageSource = "user1.png";
    }

    public void SwitchtoTab(string tabname)
    {
        switch (tabname)
        {
            case "tabHome":
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    tabbarShell.CurrentItem = tabHome;
                });
                break;
            case "tabRequests":
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    tabbarShell.CurrentItem = tabRequests;
                });
                break;
            case "tabProfile":
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    tabbarShell.CurrentItem = tabProfile;
                });
                break;
            case "tabOrders":
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    tabbarShell.CurrentItem = tabOrders;
                });
                break;
        }
    }

    private void toolbaritemSmartChat_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SmartChat());
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        WeakReferenceMessenger.Default.Unregister<UserToolbarMessage>(this);
    }
}
