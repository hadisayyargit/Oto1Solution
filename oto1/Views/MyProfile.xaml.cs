using Microsoft.Maui.Controls;
using oto1.Models;
using oto1.Services;
using System.Net.Http.Headers;

namespace oto1;

public partial class MyProfile : ContentPage
{
    public ServiceController MyServiceController { get; set; }
    CustomerModel m_mycustomer = new CustomerModel();
    byte[] m_imageBytes;
    public Stream m_stream;

    public MyProfile()
    {
        InitializeComponent();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

      RefreshForm();
        
        ////Thread.Sleep(5000);

    }
    private void mnuMyLocation_Clicked(object sender, EventArgs e)
    {
        // Navigation.PushAsync(new myLocations());

    }

    private void mnuMyCars_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MyCars());
        // DisplayAlert("alert", "goodbye", "cancel", flowDirection: FlowDirection.LeftToRight);
    }

    private async void mnuWallet_Clicked(object sender, EventArgs e)
    {
         
    }
  private void mnuPassword_Clicked(object sender, EventArgs e)
    {
        frameProfile.IsVisible = false;
        framePassword.IsVisible = true;
    }
    private void mnuProfileEdit_Clicked(object sender, EventArgs e)
    {
        frameProfile.IsVisible = true;
        framePassword.IsVisible = false;
    }


    private async void btnUpdateCustomer_Clicked(object sender, EventArgs e)
    {

        string filename = "";
        try
        {
            /// uploading
            if (m_imageBytes != null && m_imageBytes.Length > 0)
            {

                filename = $"c{GlobalClass.m_CustomerId.ToString()}.png";

                string resultUpload = await this.MyServiceController.UploadImageAsync(m_imageBytes, "customer", filename);

                if (resultUpload.StartsWith("Error") || resultUpload.StartsWith("Exception"))
                {
                    await Shell.Current.DisplayAlert("خطا در آپلود", resultUpload, "OK");
                }
                else
                {
                    string imagePathForDb = resultUpload;
                }
            }

            // ۲. آپدیت اطلاعات شخص
            await this.MyServiceController.UpdateCustomer(m_mycustomer);
            await DisplayAlert("پیام", "مشخصاتت اصلاح شد", "قبول");
        }
        catch (Exception ex)
        {
            await DisplayAlert("خطا", ex.Message, "قبول");
        }

    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        frameProfile.IsVisible = false;
        framePassword.IsVisible = false;
    }

    private async void btnSavePassword_Clicked(object sender, EventArgs e)
    {
        if (txtNewPassword.Text == "" || txtNewPassword.Text == null)
        {
            await DisplayAlert("هشدار", "گذرواژه رو وارد کن", "تایید");
            txtPassword.Focus();
            return;
        }

        if (txtConfirmPassword.Text!=txtNewPassword.Text)
        {
            await DisplayAlert("هشدار", "گذرواژه با تکرار آن متفاوت است", "تایید");
            txtConfirmPassword.Focus();
            return;
        }
        try
        {
            bool res=  await MyServiceController.UpdateUserPassword(GlobalClass.m_UserId, txtPassword.Text,txtNewPassword.Text);
            if (res)
                await DisplayAlert("", "گذرواژه تغییر کرد", "قبول", flowDirection: FlowDirection.LeftToRight);
            else
                await DisplayAlert("", "خطا: گذرواژه اشتباه است", "قبول", flowDirection: FlowDirection.LeftToRight);

        }
        catch (Exception ex)
        {
            await DisplayAlert("",ex.Message, "قبول", flowDirection: FlowDirection.LeftToRight);
        }
    }
    private async void btnSelectImage_Clicked(object sender, EventArgs e)
    {
        try
        {
            FileResult photo = await MediaPicker.PickPhotoAsync();
            if (photo != null)
            {
                // خواندن مستقیم استریم و تبدیل به بایت به صورت کاملاً امن و Async
                using (var stream = await photo.OpenReadAsync())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        m_imageBytes = memoryStream.ToArray();
                    }
                }

                await Task.Delay(200);
                // نمایش عکس در UI (برای نمایش مجدد از بایت‌ها استفاده می‌کنیم تا استریم بسته نشود)
                if (m_imageBytes != null && m_imageBytes.Length > 0)
                {
                    imgUser.Source = ImageSource.FromStream(() => new MemoryStream(m_imageBytes));
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("خطا", $"خطا در انتخاب عکس: {ex.Message}", "قبول");
        }

    }

    private void toolbaritemRefresh_Clicked(object sender, EventArgs e)
    {
        RefreshForm();
    }

    private async void RefreshForm()
    {
        frameProfile.IsVisible = false;
        framePassword.IsVisible = false;

        if (GlobalClass.m_UserId == "")
        {
            imgUser.Source = "user5.png";
            labelUsername.Text = "";


        }
        else
        {
            try
            {

                string fileUrl = $"https://khordadnet.ir/mysites/oto1/assets/customer/c{GlobalClass.m_CustomerId.ToString()}.png";

                bool fileExists = await NetClass.CheckNetFileExists(fileUrl);
                if (fileExists)
                {
                    using (var client = new HttpClient())
                    {
                        var bytes = await client.GetByteArrayAsync(fileUrl);
                        imgUser.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
                    }
                }

                labelUsername.Text = GlobalClass.m_CustomerName;


                MyServiceController = new ServiceController();


                m_mycustomer = await MyServiceController.GetCustomerByUserId(GlobalClass.m_UserId);

                if (m_mycustomer != null) 
                {
                    //روش اول
                    this.BindingContext = m_mycustomer;

                    /*
                    //روش دوم
                    txtLName.BindingContext = m_mycustomer;
                    txtLName.SetBinding(Entry.TextProperty, "LName");
                    */
                }

            }
            catch (Exception ex)
            {


            }
        }
    }

    private void streamtobytes()
    {

        //// تبدیل عکس به بایت
        using (var memoryStream = new MemoryStream())
        {
            m_stream.CopyToAsync(memoryStream);
            m_imageBytes = memoryStream.ToArray();
        }

    }

  
}