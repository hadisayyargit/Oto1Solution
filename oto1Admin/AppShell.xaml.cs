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
        // کد برای مدیریت رویداد ناوبری قبل از تغییر تب
        //Console.WriteLine("Navigating from: " + e.Source.ToString());
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

        if (GlobalClass.m_UserId == "" || GlobalClass.m_PersonId == null)
            toolbaritemUser.IconImageSource = "user5.png";
        else
        {
            string fileUrl = "https://khordadnet.ir/mysites/oto1/assets/person/a" + GlobalClass.m_PersonId.ToString() + ".png";
            bool fileExists = await NetClass.CheckNetFileExists(fileUrl);
            if (fileExists) toolbaritemUser.IconImageSource = fileUrl;
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
        }
    }

}
