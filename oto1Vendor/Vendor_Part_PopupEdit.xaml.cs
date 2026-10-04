using CommunityToolkit.Maui.Views;
using MauiPersianToolkit;
using oto1.Models;

namespace oto1;

public partial class Vendor_Part_PopupEdit : Popup
{
    private readonly PartVendorModel _part;

    public Vendor_Part_PopupEdit(PartVendorModel part)
    {
        InitializeComponent();

        _part = part;

        // نمایش اطلاعات فعلی
        txtFName.Text = part.FName;
        txtLName.Text = part.LName;
        txtPriceAmount.Text = part.PriceAmount?.ToString();
        txtDiscountPercent.Text = part.DiscountPercent?.ToString();
        txtExistance.Text = part.Existance?.ToString();
       
        dtPickerBegin.SelectedPersianDate=part.jalaliBeginDate;
        dtPickerEnd.SelectedPersianDate = part.JalaliEndDate;
    }

    private async void btnSave_Clicked(object sender, EventArgs e)
    {
        _part.FName = txtFName.Text?.Trim() ?? string.Empty;
        _part.LName = string.IsNullOrWhiteSpace(txtLName.Text)
            ? null
            : txtLName.Text.Trim();

        if (int.TryParse(txtPriceAmount.Text, out int price))
            _part.PriceAmount = price;
        else
            _part.PriceAmount = null;

        if (byte.TryParse(txtDiscountPercent.Text, out byte discount))
            _part.DiscountPercent = discount;
        else
            _part.DiscountPercent = null;

        if (int.TryParse(txtExistance.Text, out int existance))
            _part.Existance = existance;
        else
            _part.Existance = null;


        string ticketdatetimeBegin = "2024-01-01";
        string ticketdatetimeEnd = DateTime.Today.ToString("yyyy-MM-dd");


        var d1 = dtPickerBegin.SelectedPersianDate;
        var d2 = dtPickerEnd.SelectedPersianDate;

        if (d1 != null)
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

            string[] ss = d1.ToString().Split("/");
            ticketdatetimeBegin = pc.ToDateTime(int.Parse(ss[0]), int.Parse(ss[1]), int.Parse(ss[2]), 0, 0, 0, 0).ToString("yyyy-MM-dd");
        }
        if (d2 != null)
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();

            string[] ss = d2.ToString().Split("/");
            ticketdatetimeEnd = pc.ToDateTime(int.Parse(ss[0]), int.Parse(ss[1]), int.Parse(ss[2]), 0, 0, 0, 0).ToString("yyyy-MM-dd");
        }

        _part.ValidBeginDate = Convert.ToDateTime(ticketdatetimeBegin);
        _part.ValidEndDate = Convert.ToDateTime(ticketdatetimeEnd);
        // برگرداندن رکورد ویرایش‌شده
        await CloseAsync();
    }

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }
}