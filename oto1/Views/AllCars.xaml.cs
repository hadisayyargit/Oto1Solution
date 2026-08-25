namespace oto1;
using oto1.Models;
using oto1.Services;
public partial class AllCars : ContentPage
{
    //public ServiceController MyServiceController { get; set; }

    //CarModel myCar=new CarModel { CarId = 1 ,PName=""};

    List<CarModel> mycarlist = new List<CarModel>();
    ServiceController MyServiceController = new ServiceController();

    public AllCars()
	{
		InitializeComponent();
	}
    protected override  void OnAppearing()
    {
        base.OnAppearing();

 

    }

    private void collectionviewCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var cars = e.CurrentSelection.FirstOrDefault() as CarModel;
        if (cars != null) return;
        DisplayAlert("", "", "ok");

    }

    private void btnTest_Clicked(object sender, EventArgs e)
    {
        
    }

    private async void btnAllcars_Clicked(object sender, EventArgs e)
    {
        activityIndicator.IsRunning = true;
        if (NetClass.CheckNetConnection() == false)
        {
            await DisplayAlert("پیام", "Internet Connection Fail", "ok");
        }

        // mycarlist = await myCar.GetAllCars();

        // ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true, Color=Colors.Orange };



        mycarlist = await MyServiceController.GetAllCars();




        if (mycarlist != null && mycarlist.Count > 0)
        {
            foreach (var car in mycarlist)
            {

                //pr.PhotoFileName = "https://khordadnet.ir/mysites/oto1/assets/part/prt" + pr.PartId.ToString() + ".png";
               // car.ThumbnailPhotoFile = "Resources/images/car/" + car.ThumbnailPhotoFile;
                car.ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/car/car" + car.CarId.ToString() + ".png";

            }
        }


        collectionviewCars.ItemsSource = mycarlist;

        activityIndicator.IsRunning = false;
    }
}