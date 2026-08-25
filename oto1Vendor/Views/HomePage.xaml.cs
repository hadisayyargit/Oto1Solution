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

        /*
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
        */
    }
    private  async void btnMyRequests_Clicked(object sender, EventArgs e)
    {
       
        //Navigation.PushAsync(new AppShell());

        //await Shell.Current.GoToAsync("//myRequests");
        //Shell.Current.CurrentItem =
        //AppShell.tabar.FindByName<TabBar>("tabbarMyRequests");

        /*
        TabbedPage aa = (TabbedPage) this.Parent;
         aa.CurrentPage=aa.Children[1];
        */

        ((AppShell)App.Current.MainPage).SwitchtoTab("tabRequest");
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("پیام", sender.ToString(), "ok");
    }


}