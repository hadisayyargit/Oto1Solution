using CommunityToolkit.Maui.Extensions;
using oto1.Models;
using oto1.Services;
using System.ComponentModel;
using System.Globalization;


namespace oto1;

public partial class Vendor_Part : ContentPage
{
    ServiceController MyServiceController = new ServiceController();
    public List<PartVendorViewModel> PartVendorList { get; set; } = new ();
    public PartVendorViewModel SelectedPartVendor { get; set; } 
    public Command RefreshCommand { get; set; }

    private const int PageSize = 10;
    private int currentPage = 0;

    private List<PartVendorViewModel> filteredPartVendorList = new();

    private bool _isRefreshing;

    public bool IsRefreshing
    {
        get => _isRefreshing;
        set
        {
            if (_isRefreshing != value)
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
    }

    public Vendor_Part()
    {
        InitializeComponent();

        RefreshCommand = new Command(async () => await RefreshForm());
        BindingContext = this;
    }

    private async void ClearForm()
    {
         PartVendorList.Clear();
    }

    private async Task RefreshForm()
    {
        IsRefreshing = true;

        activityIndicator.IsRunning = true;
        activityIndicator.IsVisible = true;

        try
        {
            var data = await MyServiceController.GetVendor_Part(GlobalClass.m_VendorId);

            PartVendorList = data ?? new List<PartVendorViewModel>();


            foreach (var item in PartVendorList)
            {
                item.ThumbnailPhotoFile =
                    "https://khordadnet.ir/mysites/oto1/assets/part/prt"
                    + item.PartId
                    + ".png";
            }

            ApplyFilter();
        }
        catch (Exception ex)
        {
            // برای اینکه متوجه شوید خطا چیست، این لاگ را در کنسول ببینید
           // System.Diagnostics.Debug.WriteLine($"Error in RefreshForm: {ex.Message}");
        }
        finally
        {
            IsRefreshing = false;
            activityIndicator.IsRunning = false;
            activityIndicator.IsVisible = false;
        }
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

            //BindingContext = PartVendorList;
            await RefreshForm();

        }
    }

    private async void btnAdd_Clicked(object sender, EventArgs e)
    {
        PartVendorViewModel part=new PartVendorViewModel();
        part.Id = 0;
        part.VendorId = GlobalClass.m_VendorId;
        var popup = new Vendor_Part_PopupEdit(part);
        //await popup.ClearBoxes();
        ///  لیست قطعات
        await popup.LoadPartsAsync();
        await this.ShowPopupAsync(popup);

        // اینجا part قبلاً توسط Popup ویرایش شده است

        //grdVendorPart.ItemsSource = null;
        //grdVendorPart.ItemsSource = PartVendorList;
        await Task.Delay(2000);
        //BindingContext = PartVendorList;

        await RefreshForm();

    }

    private void ApplyFilter()
    {
        string searchText = sbSearch.Text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            filteredPartVendorList = PartVendorList.ToList();
        }
        else
        {
            filteredPartVendorList = PartVendorList
          .Where(x =>
            (!string.IsNullOrWhiteSpace(x.PName) &&
             x.PName.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            ||
                (x.PartId != null && x.PartId.ToString(CultureInfo.InvariantCulture)
                    .Contains(searchText, StringComparison.OrdinalIgnoreCase))).ToList();
        }

        currentPage = 0;
        ShowCurrentPage();
    }

    private void ShowCurrentPage()
    {
        int pageCount = (int)Math.Ceiling(
            filteredPartVendorList.Count / (double)PageSize);

        var pageItems = filteredPartVendorList
            .Skip(currentPage * PageSize)
            .Take(PageSize)
            .ToList();

        grdVendorPart.ItemsSource = pageItems;

        lblPage.Text = pageCount == 0
            ? "صفحه ۰ از ۰"
            : $"صفحه {currentPage + 1} از {pageCount}";

        btnPrevious.IsEnabled = currentPage > 0;
        btnNext.IsEnabled = pageCount > 0 && currentPage < pageCount - 1;
    }

    private void sbSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        ApplyFilter();
    }

    private void btnPrevious_Clicked(object sender, EventArgs e)
    {
        activityIndicator.IsRunning = true;
        activityIndicator.IsVisible = true;

        if (currentPage > 0)
        {
            currentPage--;
            ShowCurrentPage();
        }

        activityIndicator.IsRunning = false;
        activityIndicator.IsVisible = false;
    }

    private void btnNext_Clicked(object sender, EventArgs e)
    {
        activityIndicator.IsRunning = true;
        activityIndicator.IsVisible = true;
        

        int pageCount = (int)Math.Ceiling(
            filteredPartVendorList.Count / (double)PageSize);

        if (currentPage < pageCount - 1)
        {
            currentPage++;
            ShowCurrentPage();
        }


        activityIndicator.IsRunning = false;
        activityIndicator.IsVisible = false;
    }

}