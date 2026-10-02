using CommunityToolkit.Maui.Views;
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

        // برگرداندن رکورد ویرایش‌شده
        await CloseAsync();
    }

    private async void btnCancel_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }
}