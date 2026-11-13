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


    private void OnNavigating(object sender, ShellNavigatingEventArgs e)
    {
        base.OnNavigating(e);

        // اگر مقصد، روتر مربوط به منوی کشویی بود
        if (e.Target.Location.OriginalString.Contains("FlyoutHomeRoute"))
        {
            e.Cancel();
            tabbarShell.CurrentItem = tabHome;
        }
        else if(e.Target.Location.OriginalString.Contains("FlyoutAboutRoute"))
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

    /*
    private async void updateUserToolbarItem(string s)
    {
        if (string.IsNullOrEmpty(GlobalClass.m_UserId) || GlobalClass.m_PersonId == null)
        {
            toolbaritemUser.IconImageSource = "user5.png";
        }
        else
        {
            // اضافه کردن Cache Buster برای اطمینان بیشتر
            string fileUrl = $"https://khordadnet.ir/mysites/oto1/assets/person/a{GlobalClass.m_PersonId}.png?v={Guid.NewGuid().ToString().Substring(0, 8)}";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // دریافت فایل به صورت Byte Array برای دور زدن تمام انواع کش
                    var imageBytes = await client.GetByteArrayAsync(fileUrl);

                    // تبدیل بایت‌ها به ImageSource
                    using (var stream = new MemoryStream(imageBytes))
                    {
                        toolbaritemUser.IconImageSource = ImageSource.FromStream(() => stream);
                    }
                }
            }
            catch (Exception ex)
            {
                // اگر دانلود با خطا مواجه شد (مثلاً فایل نبود)، از عکس پیش‌فرض استفاده کن
                System.Diagnostics.Debug.WriteLine($"Error loading image: {ex.Message}");
                toolbaritemUser.IconImageSource = "user5.png";
            }
        }
    }
    */


    
    private async void updateUserToolbarItem(string s)
    {
        toolbaritemUser.IconImageSource = "user5.png";

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
    

    private async void toolbaritemUser_Clicked(object sender, EventArgs e)
    {
        if (GlobalClass.m_UserId == "" || GlobalClass.m_UserId == null)
        {
            Navigation.PushAsync(new SignInPage());

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
            case "tabRequest":
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    tabbarShell.CurrentItem = tabRequest;
                });
                break;
            case "tabCms":
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    tabbarShell.CurrentItem = tabCms;
                });
                break;
            case "tabProfile":
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    tabbarShell.CurrentItem = tabProfile;
                });
                break;
            case "tabReport":
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    tabbarShell.CurrentItem = tabReport;
                });
                break;
        }
    }

}
