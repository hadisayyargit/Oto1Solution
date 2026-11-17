using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

using oto1.Services;

namespace oto1;

public partial class AppShell : Shell
{
  
    public class UserToolbarMessage : ValueChangedMessage<string>
    {
        public UserToolbarMessage(string value) : base(value) { }
    }




    public AppShell()
    {
        InitializeComponent();

        this.Navigating += OnNavigating;
        this.Navigated += OnNavigated;

        Routing.RegisterRoute(nameof(MapPage), typeof(MapPage));
        Routing.RegisterRoute(nameof(Requests), typeof(Requests));


        // ثبت‌نام برای دریافت پیام (داخل سازنده)
        // در فایل AppShell.xaml.cs بخش Register را اینگونه اصلاح کن:

        WeakReferenceMessenger.Default.Register<UserToolbarMessage>(this, (r, m) =>
        {

            MainThread.BeginInvokeOnMainThread(() =>
            {
                // 1. اگر پیام برای آپدیت پروفایل کاربر بود
                if (m.Value == "updateUserToolbarItem")
                {
                    updateUserToolbarItem(m.Value);
                }
                // 2. اگر پیام حاوی عدد یا متن برای سبد خرید بود (مثلاً هر چیزی که "updateBasket:" با آن شروع شود)
                else if (m.Value.StartsWith("basket:"))
                {
                    string count = m.Value.Replace("basket:", "");
                    toolbaritemBasket.Text = $"🛒 {count}";
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
    private void OnNavigating(object sender, ShellNavigatingEventArgs e)
    {
        base.OnNavigating(e);

        // اگر مقصد، روتر مربوط به منوی کشویی بود
        if (e.Target.Location.OriginalString.Contains("FlyoutHomeRoute"))
        {
            e.Cancel();
            tabbarShell.CurrentItem = tabHome;
        }
        else if (e.Target.Location.OriginalString.Contains("FlyoutAboutRoute"))
        {
            e.Cancel();
            tabbarShell.CurrentItem = tabProfile;
        }

        //MainThread.BeginInvokeOnMainThread(() =>
        //{
        //    tabbarShell.CurrentItem = tabHome;
        //});
    }

    private void OnNavigated(object sender, ShellNavigatedEventArgs e)
    {
        // کد برای مدیریت رویداد ناوبری بعد از تغییر تب
        //var currentTab = this.CurrentItem;
        //Console.WriteLine("Navigated to: " + currentTab.Title);
        // انجام عملیات مورد نظر با استفاده از currentTab
    }


    private async void updateUserToolbarItem(string s)
    {
        toolbaritemUser.IconImageSource = "user5.png";

        if (GlobalClass.m_UserId != "")
        {
            string fileUrl = $"https://khordadnet.ir/mysites/oto1/assets/customer/c{GlobalClass.m_CustomerId.ToString()}.png";

            bool fileExists = await NetClass.CheckNetFileExists(fileUrl);
            if (fileExists)
            {
               // toolbaritemUser.IconImageSource = fileUrl;

                using (var client = new HttpClient())
                {
                    var bytes = await client.GetByteArrayAsync(fileUrl);
                    toolbaritemUser.IconImageSource = ImageSource.FromStream(() => new MemoryStream(bytes));
                }
            }
        }



        if (GlobalClass.m_UserId != "" && GlobalClass.m_PersonId != null)
        {
            //string fileUrl = "https://khordadnet.ir/mysites/oto1/assets/person/a" + GlobalClass.m_PersonId.ToString() + ".png";

            string fileUrl = $"https://khordadnet.ir/mysites/oto1/assets/person/a{GlobalClass.m_PersonId}.png?v={Guid.NewGuid().ToString().Substring(0, 8)}";

            bool fileExists = await NetClass.CheckNetFileExists(fileUrl);

            if (fileExists) toolbaritemUser.IconImageSource = fileUrl;
            // toolbaritemUser.IconImageSource = "https://khordadnet.ir/mysites/oto1/assets/person/a2.png";

            /*
            using (var client = new HttpClient())
            {
                var bytes = await client.GetByteArrayAsync(url);
                toolbaritemUser.IconImageSource = ImageSource.FromStream(() => new MemoryStream(bytes));
            }
            */
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
