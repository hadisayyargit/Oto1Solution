using oto1.Models;
using oto1.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Maui.Alerts;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace oto1;

public partial class Lubricant : ContentPage
{
    ServiceController MyServiceController = new ServiceController();
    CustomerServiceController MyCustomerServiceController = new CustomerServiceController();


    List<CarModel> cars = new List<CarModel>();
    List<ManufacturerModel> manufacturers = new List<ManufacturerModel>();
    List<PartModel> products = new List<PartModel>();
    //List<ProductCarVendorModel> searchresult = new List<ProductCarVendorModel>();
    List<PartCarVendorModel> searchresult = new List<PartCarVendorModel>();

    List<VendorModel> vendors = new List<VendorModel>();
    PartModel SelectedProduct { get; set; }
    ObservableCollection<PartCarVendorModel> myItems { get; set; }

    public Lubricant()
    {
        InitializeComponent();


    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();


        if (NetClass.CheckNetConnection() == false)
        {
            await DisplayAlert("خطا", "خطا در اتصال شبکه", "ok");
        }

        // mycarlist = await myCar.GetAllCars();

        activityIndicator.IsRunning = true;

        cars = await MyServiceController.GetAllCarsView();
        manufacturers = await MyServiceController.GetAllManufacturers();
        products = await MyServiceController.GetAllParts();
        vendors = await MyServiceController.GetAllVendors();

        pickerCar.ItemsSource = cars;
        pickerManufacturer.ItemsSource = manufacturers;
        pickerProduct.ItemsSource = products;
        pickerVendor.ItemsSource = vendors;

        SelectedProduct = new PartModel { FName = "" };

        activityIndicator.IsRunning = false;
    }



    private void pickerProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SelectedProduct.PartId = pickerProduct.SelectedIndex;


        var picker = (Picker)sender;


        if (pickerProduct.SelectedIndex != -1)
        {
            //DisplayAlert("", SelectedProduct.PartId.ToString(), "ok");
            //DisplayAlert("", picker.Items[pickerProduct.SelectedIndex], "ok");
            //DisplayAlert("", ((ProductModel)picker.ItemsSource[pickerProduct.SelectedIndex]).PartId.ToString(), "ok");

        }


    }

    private void pickerCar_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void pickerManufacturer_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void btnClear_Clicked(object sender, EventArgs e)
    {
        pickerManufacturer.SelectedIndex = -1;
        pickerCar.SelectedIndex = -1;
        pickerProduct.SelectedIndex = -1;
        txtPerformanceLevel.Text = "";
        txtViscosity.Text = string.Empty;
        pickerVendor.SelectedIndex = -1;

        collectionviewProducts.ItemsSource = null;

    }
    
    private async void btnSearch_Clicked(object sender, EventArgs e)
    {
        int PartId = -1, manufacturerid = -1, carid = -1,vendorid=-1;
        string viscosity = "", performancelevel = "";

        activityIndicator.IsRunning = true;

        viscosity = txtViscosity.Text;
        performancelevel = txtPerformanceLevel.Text;



        if (pickerProduct.SelectedIndex >= 0)
        {
            PartId = ((PartModel)pickerProduct.ItemsSource[pickerProduct.SelectedIndex]).PartId;
            //DisplayAlert("", picker.Items[pickerProduct.SelectedIndex], "ok");
            //DisplayAlert("", ((ProductModel)pickerProduct.ItemsSource[pickerProduct.SelectedIndex]).PartId.ToString(), "ok");
        }
        if (pickerManufacturer.SelectedIndex >= 0)
        {
            manufacturerid = ((ManufacturerModel)pickerManufacturer.ItemsSource[pickerManufacturer.SelectedIndex]).ManufacturerId;
        }
        if (pickerCar.SelectedIndex >= 0)
        {
            carid = ((CarModel)pickerCar.ItemsSource[pickerCar.SelectedIndex]).CarId;

        }
        if (pickerVendor.SelectedIndex >= 0)
        {
            vendorid = ((VendorModel)pickerVendor.ItemsSource[pickerVendor.SelectedIndex]).VendorId;

        }

        searchresult = await MyServiceController.GetPart(PartId, manufacturerid, viscosity, performancelevel, carid,vendorid);


        if (searchresult != null && searchresult.Count > 0)
        {
            foreach (var pr in searchresult)
            {

                //pr.PhotoFileName = "p" + pr.PartId.ToString() + ".png";
                pr.PhotoFileName = "https://khordadnet.ir/mysites/oto1/assets/part/prt" + pr.PartId.ToString() + ".png";

            }
        }


        collectionviewProducts.ItemsSource = searchresult;
        activityIndicator.IsRunning = false;

    }

    private async void btnAddToBasket_Clicked(object sender, EventArgs e)
    {

        PartCarVendorModel mm = new PartCarVendorModel();
        List<CustomerServiceItemViewModel> myServices = new List<CustomerServiceItemViewModel>();

        string res = "";
        string msg = "";

        // if(GlobalClass.Latitude==0 && GlobalClass.Longitude==0)

        /*
        if (GlobalClass.m_LocationAddressId == 0 )
        {

            //var toast = Toast.Make("جایگاه (موقعیت مکانی) انتخاب نشده است",CommunityToolkit.Maui.Core.ToastDuration.Short, 12);
            //await toast.Show();

            await DisplayAlert("", "جایگاه (موقعیت مکانی) انتخاب نشده است", "ok");
            return;

        }
        */

        var allItems = collectionviewProducts.ItemsSource;
        
        if (allItems != null)
        {
            foreach (PartCarVendorModel item in allItems)
            {
                if (item.Quantity > 0)
                {
                    myServices.Add(new CustomerServiceItemViewModel() { CustomerId = GlobalClass.m_CustomerId, PartId = item.PartId, Quantity = item.Quantity, ServiceItemType = (byte)GlobalClass.enumServiceItemType.serviceitemtype_PartSale, ServiceItemStatus = (byte)GlobalClass.enumServiceItemStatus.serviceitemstatus_Request, NetAmount = item.NetAmount, PriceAmount=item.PriceAmount, VendorId = item.VendorId , Latitude = GlobalClass.m_Latitude, Longitude=GlobalClass.m_Longitude,Address=GlobalClass.m_Address });                
                }
            }
            res = await MyCustomerServiceController.AddCustomerService(myServices);
            msg = "درخواست شما ثبت شد با شماره‌های: " + res;
            await DisplayAlert("", msg, "ok");
        }

    }

    private void pickerVendor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private async void btnBeforeStep_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();

    }

    
}