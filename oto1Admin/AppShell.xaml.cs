
namespace oto1;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        this.Navigating += OnNavigating;
        this.Navigated += OnNavigated;

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

    private void updateUserToolbarItem(string s)
    {

        if (GlobalClass.m_UserId == "" || GlobalClass.m_PersonId == null)
            toolbaritemUser.IconImageSource = "user5.png";
        else
            toolbaritemUser.IconImageSource = "https://khordadnet.ir/mysites/oto1/assets/person/a" + GlobalClass.m_PersonId.ToString() + ".png";

    }


    private void toolbaritemUser_Clicked(object sender, EventArgs e)
    {
        if (GlobalClass.m_UserId == "" || GlobalClass.m_UserId == null)
        {
            Navigation.PushAsync(new SignInPage());

        }
        else
        {
            toolbaritemLogoff_Clicked(this, null);
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
