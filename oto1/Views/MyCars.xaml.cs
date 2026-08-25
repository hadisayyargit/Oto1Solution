using oto1.Models;
using oto1.Services;

namespace oto1;

public partial class MyCars : ContentPage
{
    //public ServiceController MyServiceController { get; set; }

    //CarModel myCar=new CarModel { CarId = 1 ,PName=""};


    ServiceController MyServiceController = new ServiceController();

    public MyCars()
    {
        InitializeComponent();

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();


        if (NetClass.CheckNetConnection() == false)
        {
            await DisplayAlert("پیام", "Internet Connection Fail", "ok");
        }

        // mycarlist = await myCar.GetAllCars();

        // ActivityIndicator activityIndicator = new ActivityIndicator { IsRunning = true, Color=Colors.Orange };



        List<Customer_CarModel>  mycarlist = await MyServiceController.GetMyCars(1);


        if (mycarlist != null && mycarlist.Count > 0)
        {
            foreach (var car in mycarlist)
            {


                string s = "";
                try
                {
                    s = Convert.ToBase64String(car.ThumbnailPhoto);
                }
                catch
                {
                }
                car.ThumbnailPhotoString = string.Format("data:image/jpeg;base64,{0}", s);

            }
        }
     

        collectionviewCars.ItemsSource = mycarlist;


        List<CarModel>  carList = await MyServiceController.GetAllCarsView();

        pickerCar.ItemsSource = carList;
        activityIndicator.IsRunning = false;

    }


    private void collectionviewCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Customer_CarModel car =(Customer_CarModel)e.CurrentSelection.FirstOrDefault();
        if (car == null) return;
        DisplayAlert("", car.FName, "ok");
        ((CollectionView)sender).SelectedItem = null;
    }

    private void pickerCar_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void btnUpdate_Clicked(object sender, EventArgs e)
    {

    }

    private void btnClear_Clicked(object sender, EventArgs e)
    {

    }
}