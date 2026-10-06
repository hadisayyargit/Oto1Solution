using CommunityToolkit.Maui.Extensions;
using oto1.Models;
using oto1.Services;
using System.ComponentModel;


namespace oto1;

public partial class Vendor_Part : ContentPage
{
    ServiceController MyServiceController = new ServiceController();
    public List<PartVendorViewModel> PartVendorList { get; set; } = new ();
    public bool IsRefreshing { get; set; }
    public PartVendorViewModel SelectedPartVendor { get; set; } 
    public Command RefreshCommand { get; set; }
    public Vendor_Part()
    {
        RefreshCommand = new Command(async () =>
        {
            IsRefreshing = true;
            await Task.Delay(2000);
          //  await RefreshForm();

            IsRefreshing = false;
            //OnPropertyChanged(nameof(IsRefreshing));

        });

        //BindingContext = this;
        InitializeComponent();
	}

    private async void ClearForm()
    {
         PartVendorList.Clear();
    }

    private async Task RefreshForm()
    {
        activityIndicator.IsRunning = true;
        activityIndicator.IsVisible = true;

        PartVendorList = await MyServiceController.GetVendor_Part(GlobalClass.m_VendorId);
        foreach (var item in PartVendorList)
        {
            item.ThumbnailPhotoFile = "https://khordadnet.ir/mysites/oto1/assets/part/prt" + item.PartId.ToString() + ".png";
        }
        
        await Task.Delay(2000);

        BindingContext = PartVendorList;

        //BindingContext = this;
        activityIndicator.IsRunning = false;
        activityIndicator.IsVisible = false;
    }

    private void btnClear_Clicked(object sender, EventArgs e)
    {
        PartVendorViewModel item1 = this.SelectedPartVendor;
        ClearForm();
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
         await RefreshForm();
    }

    private async void btnEdit_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button &&
            button.BindingContext is PartVendorViewModel part)
        {
            
            var popup = new Vendor_Part_PopupEdit(part);
            ///  لیست قطعات
            await popup.LoadPartsAsync();
            await this.ShowPopupAsync(popup);

            // اینجا part قبلاً توسط Popup ویرایش شده است

            //grdVendorPart.ItemsSource = null;
            //grdVendorPart.ItemsSource = PartVendorList;
            await Task.Delay(2000);

            BindingContext = PartVendorList;
        }
    }

    private async void btnAdd_Clicked(object sender, EventArgs e)
    {
        PartVendorViewModel part=new PartVendorViewModel();
        part.Id = 0;
        part.VendorId = GlobalClass.m_VendorId;
        var popup = new Vendor_Part_PopupEdit(part);
        ///  لیست قطعات
        await popup.LoadPartsAsync();
        await this.ShowPopupAsync(popup);

        // اینجا part قبلاً توسط Popup ویرایش شده است

        //grdVendorPart.ItemsSource = null;
        //grdVendorPart.ItemsSource = PartVendorList;
        await Task.Delay(2000);
        BindingContext = PartVendorList;
    }
}