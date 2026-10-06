using CommunityToolkit.Maui.Views;
using oto1.Models;
using oto1.Services;
using System.Globalization;

namespace oto1;

public partial class Vendor_Part_PopupEdit : Popup
{
    private readonly PartVendorViewModel _part;

    public ServiceController MyServiceController { get; set; }

    public List<PartModel> allparts { get; set; } = new List<PartModel>();

    public Vendor_Part_PopupEdit(PartVendorViewModel part)
    {
        InitializeComponent();

        _part = part;

        MyServiceController = new ServiceController();

        // اطلاعات رکورد
        txtId.Text = _part.Id.ToString();
        txtLName.Text = part.LName;
        txtPriceAmount.Text = part.PriceAmount?.ToString();
        txtDiscountPercent.Text = part.DiscountPercent?.ToString();
        txtExistance.Text = part.Existance?.ToString();

        dtPickerBegin.SelectedPersianDate = part.jalaliBeginDate;
        dtPickerEnd.SelectedPersianDate = part.JalaliEndDate;
    }


    public async Task LoadPartsAsync()
    {
        // گرفتن لیست قطعات
        allparts = await MyServiceController.GetAllParts();

        // پر کردن Picker
        pickerPart.ItemsSource = allparts;

        // پیدا کردن قطعه فعلی
        var selectedPart = allparts.FirstOrDefault(x => x.PartId == _part.PartId);

        if (selectedPart != null)
        {
            pickerPart.SelectedItem = selectedPart;
            txtLName.Text = selectedPart.LName;
        }
        else
        {
            pickerPart.SelectedItem = null;
            txtLName.Text = "";
        }
    }


    private void pickerPart_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (pickerPart.SelectedItem is PartModel selectedPart)
        {
            _part.PartId = selectedPart.PartId;
            _part.FName = selectedPart.FName;
            _part.LName = selectedPart.LName;
            txtLName.Text = selectedPart.LName;
        }
    }


    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        try
        {
           
            _part.Id = int.Parse(txtId.Text);

            _part.PriceAmount =
                int.TryParse(txtPriceAmount.Text, out int price)
                    ? price : null;

            _part.DiscountPercent =
                byte.TryParse(txtDiscountPercent.Text, out byte discount)
                    ? discount                    : null;

            _part.Existance =
                int.TryParse(txtExistance.Text, out int existance)
                    ? existance                    : null;


            // تاریخ
            string datetimeBegin = "2024-01-01";
            string datetimeEnd = DateTime.Today.ToString("yyyy-MM-dd");

            PersianCalendar pc = new PersianCalendar();

            var d1 = dtPickerBegin.SelectedPersianDate;

            if (!string.IsNullOrWhiteSpace(d1))
            {
                string[] ss = d1.Split('/');

                DateTime dateBegin = pc.ToDateTime(
                    int.Parse(ss[0]),
                    int.Parse(ss[1]),
                    int.Parse(ss[2]),
                    0, 0, 0, 0);

                datetimeBegin = dateBegin.ToString("yyyy-MM-dd");
            }

            var d2 = dtPickerEnd.SelectedPersianDate;

            if (!string.IsNullOrWhiteSpace(d2))
            {
                string[] ss = d2.Split('/');

                DateTime dateEnd = pc.ToDateTime(
                    int.Parse(ss[0]),
                    int.Parse(ss[1]),
                    int.Parse(ss[2]),
                    0, 0, 0, 0);

                datetimeEnd = dateEnd.ToString("yyyy-MM-dd");
            }


            Vendor_PartModel myVendorpart = new Vendor_PartModel
            {
                Id = _part.Id,
                VendorId = _part.VendorId,
                PartId = _part.PartId,
                ValidBeginDate = Convert.ToDateTime(datetimeBegin),
                ValidEndDate = Convert.ToDateTime(datetimeEnd),
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
            await Shell.Current.DisplayAlert(
                "خطا",
                ex.Message,
                "باشه");
        }
    }


    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }
}