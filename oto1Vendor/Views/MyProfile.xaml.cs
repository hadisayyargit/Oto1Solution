using oto1.Models;
using oto1.Services;

namespace oto1;

public partial class MyProfile : ContentPage
{
    public ServiceController MyServiceController { get; set; }
    VendorModel m_myvendor = new VendorModel();
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

    }
    private void mnuMyLocation_Clicked(object sender, EventArgs e)
    {
        // Navigation.PushAsync(new myLocations());

    }


    private void mnuMyCars_Clicked(object sender, EventArgs e)
    {
        //Navigation.PushAsync(new MyCars());
        // DisplayAlert("alert", "goodbye", "cancel", flowDirection: FlowDirection.LeftToRight);
    }



    private async void mnuWallet_Clicked(object sender, EventArgs e)
    {
         await TextToSpeech.SpeakAsync("Oto1");
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        ((AppShell)App.Current.MainPage).SwitchtoTab("tabHome");
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

    private async void btnUpdateVendor_Clicked(object sender, EventArgs e)
    {

        string resultUpload = "";

        if (m_imageBytes != null)
        {
            using (var memoryStream = new MemoryStream())
            {

                memoryStream.WriteAsync(m_imageBytes, 0, m_imageBytes.Length);

                //await m_stream.CopyToAsync(memoryStream);

                memoryStream.Position = 0;

                string imageURL = "https://khordadnet.ir/mysites/oto1/assets/vendor";
                //imageURL = "https://example.com/upload/";
                //imageURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/";



                //resultUpload = await this.MyServiceController.UploadImageAsync(memoryStream, imageURL);


                /*

                using (var client = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    var streamContent = new StreamContent(memoryStream);
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    content.Add(streamContent, "file", "cst2.jpg");

                    try
                    {
                        var response = await client.PostAsync(imageURL, content);
                        response.EnsureSuccessStatusCode();

                        if (response.IsSuccessStatusCode)
                        {
                            resultUpload = "1";
                        }
                        else
                        {
                            resultUpload = "0";
                        }
                    }
                    catch (Exception ex)
                    {
                        resultUpload = "0";
                    }
                }
                */

            }

        }

        try
        {
            await this.MyServiceController.UpdateVendor(m_myvendor);
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

        if (txtConfirmPassword.Text != txtNewPassword.Text)
        {
            await DisplayAlert("هشدار", "گذرواژه با تکرار آن متفاوت است", "تایید");
            txtConfirmPassword.Focus();
            return;
        }
        try
        {
            bool res = await MyServiceController.UpdateUserPassword(GlobalClass.m_UserId, txtPassword.Text, txtNewPassword.Text);
            if (res)
                await DisplayAlert("", "گذرواژه تغییر کرد", "قبول", flowDirection: FlowDirection.LeftToRight);
            else
                await DisplayAlert("", "خطا: گذرواژه اشتباه است", "قبول", flowDirection: FlowDirection.LeftToRight);

        }
        catch (Exception ex)
        {
            await DisplayAlert("", ex.Message, "قبول", flowDirection: FlowDirection.LeftToRight);
        }
    }
    private async void btnSelectImage_Clicked(object sender, EventArgs e)
    {


        FileResult photo = await MediaPicker.PickPhotoAsync();
        if (photo != null)
        {
            m_stream = await photo.OpenReadAsync();



            if (m_stream != null)
            {
                streamtobytes();
                m_stream.Position = 0;


                await Task.Delay(200);

                imgUser.Source = ImageSource.FromStream(() => m_stream);


            }



        }
    }

    private void toolbaritemRefresh_Clicked(object sender, EventArgs e)
    {
        RefreshForm();
    }

    private async void RefreshForm()
    {
        if (GlobalClass.m_UserId == "")
        {
            imgUser.Source = "user5.png";
            labelUsername.Text = "";


        }
        else
        {
            try
            {
                //imgUser.Source = GlobalClass.m_UserThumbnailPhotoString;
                imgUser.Source = "https://khordadnet.ir/mysites/oto1/assets/vendor/v" + GlobalClass.m_VendorId.ToString() + ".png";

                labelUsername.Text = GlobalClass.m_VendorName;


                MyServiceController = new ServiceController();


                m_myvendor = await MyServiceController.GetVendorByUserId(GlobalClass.m_UserId);

                if (m_myvendor != null)
                {
                    //روش اول
                    this.BindingContext = m_myvendor;

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

    private void mnuVendor_Part_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Vendor_Part());
    }
}