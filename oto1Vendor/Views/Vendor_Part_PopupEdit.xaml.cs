using CommunityToolkit.Maui.Views;
using oto1.Models;
using oto1.Services;
using System.Collections.ObjectModel; // اضافه شد
using System.Globalization;

namespace oto1;

public partial class Vendor_Part_PopupEdit : Popup
{
    private readonly PartVendorViewModel _part;
    public ServiceController MyServiceController { get; set; }

    public List<PartModel> allparts { get; set; } = new List<PartModel>();

    // لیست جدید برای نمایش در CollectionView
    public ObservableCollection<PartModel> FilteredParts { get; set; } = new ObservableCollection<PartModel>();

    public Vendor_Part_PopupEdit(PartVendorViewModel part)
    {
        InitializeComponent();
        _part = part;
        MyServiceController = new ServiceController();

        txtId.Text = _part.Id.ToString();
        txtLName.Text = part.LName;
        txtPriceAmount.Text = part.PriceAmount?.ToString();
        txtDiscountPercent.Text = part.DiscountPercent?.ToString();
        txtExistance.Text = part.Existance?.ToString();

        // اتصال CollectionView به لیست فیلتر شده
        cvPartResults.ItemsSource = FilteredParts;
    }

    public async Task ClearBoxes()
    {
        //pickerPart.SelectedIndex = -1;
        txtPriceAmount.Text = "";
        txtDiscountPercent.Text = "";
        txtExistance.Text = "";
    }
    public async Task LoadPartsAsync()
    {
        allparts = await MyServiceController.GetAllParts();

        // مقداردهی اولیه لیست فیلتر شده
        FilteredParts.Clear();
        foreach (var item in allparts)
            FilteredParts.Add(item);

        // پیدا کردن قطعه فعلی برای نمایش اولیه
        var selectedPart = allparts.FirstOrDefault(x => x.PartId == _part.PartId);
        if (selectedPart != null)
        {
            _part.PartId = selectedPart.PartId;
            _part.FName = selectedPart.FName;
            _part.LName = selectedPart.LName;
            txtLName.Text = selectedPart.LName;
            sbPart.Text = selectedPart.FName; // نمایش نام در سرچ‌بار
        }
    }

    // منطق جستجو
    private void OnPartSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string searchText = e.NewTextValue;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            FilteredParts.Clear();
            foreach (var item in allparts) FilteredParts.Add(item);
            cvPartResults.IsVisible = false;
        }
        else
        {
            var filtered = allparts
                .Where(x => x.FName.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredParts.Clear();
            foreach (var item in filtered) FilteredParts.Add(item);

            cvPartResults.IsVisible = filtered.Any();
        }
    }

    // وقتی کاربر از لیست انتخاب می‌کند
    private void OnPartSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is PartModel selectedPart)
        {
            _part.PartId = selectedPart.PartId;
            _part.FName = selectedPart.FName;
            _part.LName = selectedPart.LName;

            txtLName.Text = selectedPart.LName;
            sbPart.Text = selectedPart.FName; // نمایش نام در سرچ‌بار

            cvPartResults.IsVisible = false; // بستن لیست
            cvPartResults.SelectedItem = null; // ریست کردن انتخاب
        }
    }

    private async void btnCancel_Clicked(object sender, EventArgs e) => await CloseAsync();

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(sbPart.Text))
        {
            await Shell.Current.DisplayAlert("تایید", "لطفا قطعه را از لیست انتخاب کن", "قبول");
            return;
        }

        try
        {
            _part.Id = int.Parse(txtId.Text);
            _part.PriceAmount = int.TryParse(txtPriceAmount.Text, out int price) ? price : null;
            _part.DiscountPercent = byte.TryParse(txtDiscountPercent.Text, out byte discount) ? discount : null;
            _part.Existance = int.TryParse(txtExistance.Text, out int existance) ? existance : null;


            Vendor_PartModel myVendorpart = new Vendor_PartModel
            {
                Id = _part.Id,
                VendorId = _part.VendorId,
                PartId = _part.PartId,
                PriceAmount = _part.PriceAmount,
                DiscountPercent = _part.DiscountPercent,
                Existance = _part.Existance


            };


            if (myVendorpart.Id == 0)
            {
                await MyServiceController.AddVendorPart(myVendorpart);
            }
            else
            {
                await MyServiceController.UpdateVendorPart(myVendorpart);
            }

            await CloseAsync();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("خطا", ex.Message, "باشه");
        }
    }
}
