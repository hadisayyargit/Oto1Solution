
using oto1.Services;

namespace oto1;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        this.Navigating += OnNavigating;
        this.Navigated += OnNavigated;

        //MessagingCenter.Subscribe<myRequests,string>(this, "UpdateToolbarItem", (sender,item) =>
        //{
        //    UpdateToolbarItem(item);

        //});


        ///<HADI 14050503></HADI>
        //MessagingCenter.Subscribe<SignInPage, string>(this, "updateUserToolbarItem", (sender, item) =>
        //{
        //    updateUserToolbarItem(item);

        //});

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

    //private void UpdateToolbarItem(string s)
    //{

    //    toolbaritemBasket.Text = s;
    //}

    private async void updateUserToolbarItem(string s)
    {
        toolbaritemUser.IconImageSource = "user5.png";

        if (GlobalClass.m_UserId != "")
        {
            string fileUrl = "https://khordadnet.ir/mysites/oto1/assets/vendor/v" + GlobalClass.m_VendorId.ToString() + ".png";

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

    private void toolbaritemBasket_Clicked(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            tabbarShell.CurrentItem = tabRequest;



            var aa = this.CurrentPage;
            // aa.CurrentPage = aa.Children[1];
        });
    }
}
