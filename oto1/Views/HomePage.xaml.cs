namespace oto1;
using CommunityToolkit.Mvvm.Messaging;
using oto1.Models;
using oto1.Services;

public partial class HomePage : ContentPage
{
    public ServiceController MyServiceController { get; set; }
    //public List<ProductModel> SlideList;
    public List<PartModel> SlideList { get; private set; }
    public HomePage()
	{
		InitializeComponent();

        //WeakReferenceMessenger.Default.Send(new AppShell.UserToolbarMessage("basket:7"));
        WeakReferenceMessenger.Default.Send(new AppShell.UserToolbarMessage("basket:"));
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
            PName = "خرید روغن موتور",
            ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/cms/prt6.png"
            ,PartAttribute=((byte)GlobalClass.enumServiceItemType.serviceitemtype_LubricantSale).ToString()
        });

        SlideList.Add(new PartModel
        {
            PName = "خرید باتری خودرو همراه با ارسال و نصب رایگان",
            ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/cms/battery1.png"
           ,
            PartAttribute =((byte) GlobalClass.enumServiceItemType.serviceitemtype_BatterySale).ToString()
        });


        SlideList.Add(new PartModel
        {
            PName = "خرید لاستیک به قیمت دولتی",
            //ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
            ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/cms/tire1.png"
           ,
            PartAttribute =((byte)GlobalClass.enumServiceItemType.serviceitemtype_tiresale).ToString()
        });

        SlideList.Add(new PartModel
        {
            PName = "خرید انواع قطعات یدکی اصلی خودرو",
            ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/cms/prt30.png"
           ,
            PartAttribute = ((byte)GlobalClass.enumServiceItemType.serviceitemtype_PartSale).ToString()
        });
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        //var a = (PartModel)sender;
        var button = sender as ImageButton;
        if (button != null)
        {
            var clickedSlide = button.BindingContext as PartModel;

            if (clickedSlide != null)
            {
                byte partattr =Convert.ToByte( clickedSlide.PartAttribute);

                /*
                switch (partattr)
                {
                    case (byte)GlobalClass.enumServiceItemType.serviceitemtype_LubricantSale:
                        //DisplayAlert("پیام", partattr.ToString(), "ok");


                        break;
                    case (byte)GlobalClass.enumServiceItemType.serviceitemtype_BatterySale:
                        DisplayAlert("پیام", partattr.ToString(), "ok");
                        break;
                    case (byte)GlobalClass.enumServiceItemType.serviceitemtype_tiresale:
                        DisplayAlert("پیام", partattr.ToString(), "ok");
                        break;
                    case (byte)GlobalClass.enumServiceItemType.serviceitemtype_PartSale:
                        DisplayAlert("پیام", partattr.ToString(), "ok");
                        break;
                }
                */

                if (GlobalClass.m_CustomerServiceItemView == null)
                    GlobalClass.m_CustomerServiceItemView = new CustomerServiceItemViewModel();

                GlobalClass.m_CustomerServiceItemView.ServiceType = (byte)GlobalClass.enumServiceType.servicetype_Part;
                GlobalClass.m_CustomerServiceItemView.ServiceItemType = partattr;
                ((AppShell)App.Current.MainPage).SwitchtoTab("tabRequests");

            }
        }
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

        DisplayAlert("پیام", "این خدمت به زودی عرضه می‌گردد", "ok");
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