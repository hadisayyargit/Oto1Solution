using oto1.Models;
using oto1.Services;

namespace oto1;

public partial class MyProfile : ContentPage
{
    public ServiceController MyServiceController { get; set; }
    PersonModel m_myperson = new PersonModel();
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


        //try
        //{
        //    var photo = await MediaPicker.PickPhotoAsync();
        //    if (photo == null)
        //        return;
        //    await using var stream = await photo.OpenReadAsync();
        //    using var ms = new MemoryStream();
        //    await stream.CopyToAsync(ms);
        //    m_imageBytes = ms.ToArray();
        //    imgUser.Source = ImageSource.FromStream(() => new MemoryStream(m_imageBytes));
        //}
        //catch (Exception ex)
        //{
        //    await DisplayAlert("خطا", ex.Message, "قبول");
        //}


    }
    private async void btnUpdatePerson_Clicked(object sender, EventArgs e)
    {
        try
        {
            // ۱. اول آپلود عکس (اگر عکسی انتخاب شده باشد)
            if (m_imageBytes != null && m_imageBytes.Length > 0)
            {
               // string imageURL = "https://khordadnet.ir/mysites/oto1/assets/person/p100.png";



                // اصلاح متد آپلود برای گرفتن مستقیم بایت‌ها (بسیار پایدارتر)
                string resultUpload = await this.MyServiceController.UploadImageAsync(m_imageBytes, "person");

                if (resultUpload.StartsWith("Error") || resultUpload.StartsWith("Exception"))
                {
                    // نمایش خطا به کاربر
                    await Shell.Current.DisplayAlert("خطا در آپلود", resultUpload, "OK");
                }
                else
                {
                    // result حالا حاوی آدرس است، مثلاً: /assets/person/2026...jpg
                    // این آدرس را در مدل خود (مثلاً CustomerModel) برای ذخیره در دیتابیس قرار بده
                    string imagePathForDb = resultUpload;
                }
            }

            // ۲. آپدیت اطلاعات شخص
            await this.MyServiceController.UpdatePerson(m_myperson);
            await DisplayAlert("پیام", "مشخصاتت اصلاح شد", "قبول");
        }
        catch (Exception ex)
        {
            await DisplayAlert("خطا", ex.Message, "قبول");
        }

        /*
        string resultUpload = "";

        if (m_imageBytes != null)
        {
            using (var memoryStream = new MemoryStream())
            {

                await memoryStream.WriteAsync(m_imageBytes, 0, m_imageBytes.Length);

                //await m_stream.CopyToAsync(memoryStream);

                memoryStream.Position = 0;

                string imageURL = "https://khordadnet.ir/mysites/oto1/assets/person/a100.png";
                                 
                resultUpload = await this.MyServiceController.UploadImageAsync(memoryStream, imageURL);

            }

        }


        try
        {
            await this.MyServiceController.UpdatePerson(m_myperson);
            await DisplayAlert("پیام", "مشخصاتت اصلاح شد", "قبول");
        }
        catch (Exception ex)
        {
            await DisplayAlert("خطا", ex.Message, "قبول");
        }
        */
        /*
        try
        {
            if (m_imageBytes is { Length: > 0 })
            {
                // این باید اسکریپت/API دریافت فایل باشد، نه خود p100.png
                //string uploadUrl = "https://khordadnet.ir/mysites/oto1/api/upload-person.php";
                string uploadUrl = "https://khordadnet.ir/mysites/oto1/assets/person";
                string resultUpload = await MyServiceController.UploadImageAsync(
                    m_imageBytes,
                    uploadUrl,
                    "p100.jpg");
                if (resultUpload != "1")
                {
                    await DisplayAlert("خطا", "آپلود عکس انجام نشد: " + resultUpload, "قبول");
                    return;
                }
            }
            await MyServiceController.UpdatePerson(m_myperson);
            await DisplayAlert("پیام", "مشخصاتت اصلاح شد", "قبول");
        }
        catch (Exception ex)
        {
            await DisplayAlert("خطا", ex.Message, "قبول");
        }
        */
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
                //imgUser.Source = GlobalClass.m_UserThumbnailPhotoString;
                imgUser.Source = "https://khordadnet.ir/mysites/oto1/assets/person/a" + GlobalClass.m_PersonId.ToString() + ".png";

                labelUsername.Text = GlobalClass.m_PersonName;

                MyServiceController = new ServiceController();
                m_myperson = await MyServiceController.GetPersonByUserId(GlobalClass.m_UserId);

                if (m_myperson != null)
                {
                    //روش اول
                    this.BindingContext = m_myperson;

                    /*
                    //روش دوم
                    txtLName.BindingContext = m_myperson;
                    txtLName.SetBinding(Entry.TextProperty, "LName");
                    */
                }

            }
            catch (Exception ex)
            {


            }
        }
    }

    private async void streamtobytes()
    {

        //// تبدیل عکس به بایت
        using (var memoryStream = new MemoryStream())
        {
           await m_stream.CopyToAsync(memoryStream);
            m_imageBytes = memoryStream.ToArray();
        }

    }

    private void mnuUsers_Clicked(object sender, EventArgs e)
    {

    }
}