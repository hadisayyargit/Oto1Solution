namespace oto1;
using oto1.Services;
using oto1.Models;
public partial class HomePage : ContentPage
{
    public ServiceController MyServiceController { get; set; }
    //public List<ProductModel> SlideList;
    public List<PartModel> SlideList { get; private set; }
    public HomePage()
	{
		InitializeComponent();
        this.MyServiceController = new ServiceController();

        CreateOfferCollection();
        BindingContext = this;

        var timer = Application.Current.Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(4);



        timer.Tick += (s, e) =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                mainCarouselView.Position = (mainCarouselView.Position + 1) % SlideList.Count;
            });
        };
        timer.Start();
    }
    void CreateOfferCollection()
    {

        SlideList = new List<PartModel>();

        SlideList.Add(new PartModel
        {
            FName = "بهران رانا 10W-40",
            //ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
            ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/part/prt1.png"
        });

        SlideList.Add(new PartModel
        {
            FName = "اسپیدی",
            ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/part/prt4.png"
        });
        SlideList.Add(new PartModel
        {
            FName = "الوند ایرانول",
            ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/part/prt6.png"
        });


    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("پیام", sender.ToString(), "ok");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
    }

    private async void btnAutoPartsSales_Clicked(object sender, EventArgs e)
    {
        if (GlobalClass.m_UserId == "" || GlobalClass.m_UserId == null)
        {
            DisplayAlert("هشدار", "به عنوان کاربر معتبر وارد برنامه نشده‌ای", "ok");
            return;
        }
        else
        {
            if (GlobalClass.m_CustomerServiceItemView == null)
                GlobalClass.m_CustomerServiceItemView = new CustomerServiceItemViewModel();

            GlobalClass.m_CustomerServiceItemView.ServiceType = (byte)GlobalClass.enumServiceType.servicetype_Part;

            ((AppShell)App.Current.MainPage).SwitchtoTab("tabRequests");
           // await Shell.Current.Navigation.PushAsync(new Requests());

        }
    }

    private void btnSpecialServices_Clicked(object sender, EventArgs e)
    {
        if (GlobalClass.m_CustomerServiceItemView == null)
            GlobalClass.m_CustomerServiceItemView = new CustomerServiceItemViewModel();

        GlobalClass.m_CustomerServiceItemView.ServiceType = (byte)GlobalClass.enumServiceType.servicetype_SpecialService;

        DisplayAlert("پیام", "این گزینه هنوز پیاده‌سازی نشده است", "ok");
    }

    private void btnTowing_Clicked(object sender, EventArgs e)
    {
        if (GlobalClass.m_UserId == "" || GlobalClass.m_UserId == null)
        {
            DisplayAlert("هشدار", "به عنوان کاربر معتبر وارد برنامه نشده‌ای", "ok");
            return;
        }
        else
        {
            if (GlobalClass.m_CustomerServiceItemView == null)
                GlobalClass.m_CustomerServiceItemView = new CustomerServiceItemViewModel();

            GlobalClass.m_CustomerServiceItemView.ServiceType = (byte)GlobalClass.enumServiceType.servicetype_CarTowing;

            ((AppShell)App.Current.MainPage).SwitchtoTab("tabRequests");
        //    Shell.Current.Navigation.PushAsync(new Requests());

        }
    }

    private void btnLocationServices_Clicked(object sender, EventArgs e)
    {

        //Navigation.PushAsync(new AppShell());

        //await Shell.Current.GoToAsync("//myRequests");
        //Shell.Current.CurrentItem =
        //AppShell.tabar.FindByName<TabBar>("tabRequests");

        // TabbedPage aa = (TabbedPage) this.Parent;
        //  aa.CurrentPage=aa.Children[1];

        ///روش قدیمی و سنتی
        //Navigation.PushAsync(new Requests());

        ///روش جدید برپایه 
        ///AppShell
        ///در صورتیکه در آن یک “آدرس”
        ///(Route) 
        ///بدهیم  
        ///


        /*
        if (sender is ImageButton { CommandParameter: string goldservices })
        {
            await Shell.Current.GoToAsync($"{nameof(Requests)}?service={goldservices}");
        }

        */

        if (GlobalClass.m_UserId == "" || GlobalClass.m_UserId == null)
        {
            DisplayAlert("هشدار", "به عنوان کاربر معتبر وارد برنامه نشده‌ای", "ok");
            return;
        }
        else
        {
            if (GlobalClass.m_CustomerServiceItemView == null)
                GlobalClass.m_CustomerServiceItemView = new CustomerServiceItemViewModel();

            GlobalClass.m_CustomerServiceItemView.ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnLocationService;

            ((AppShell)App.Current.MainPage).SwitchtoTab("tabRequests");
          //  Shell.Current.Navigation.PushAsync(new Requests());

        }
    }

    private void btnSiteServices_Clicked(object sender, EventArgs e)
    {
        if (GlobalClass.m_UserId == "" || GlobalClass.m_UserId == null)
        {
            DisplayAlert("هشدار", "به عنوان کاربر معتبر وارد برنامه نشده‌ای", "ok");
            return;
        }
        else
        {
            if (GlobalClass.m_CustomerServiceItemView == null)
                GlobalClass.m_CustomerServiceItemView = new CustomerServiceItemViewModel();

            GlobalClass.m_CustomerServiceItemView.ServiceType = (byte)GlobalClass.enumServiceType.servicetype_OnSiteService;

            ((AppShell)App.Current.MainPage).SwitchtoTab("tabRequests");
        //    Shell.Current.Navigation.PushAsync(new Requests());

        }
    }
}