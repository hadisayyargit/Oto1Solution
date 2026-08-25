using Microsoft.Maui;
using Microsoft.Maui.Controls;
using oto1.Models;
using oto1.Services;
//using Security;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
//using Windows.Graphics.Imaging;

namespace oto1;

public partial class SignUp : ContentPage
{
    byte[] m_imageBytes;
    public Stream m_stream;

    public ServiceController MyServiceController { get; set; }

    public SignUp()
    {
        InitializeComponent();
        this.MyServiceController = new ServiceController();
    }

    private async void btnSignUp_Clicked(object sender, EventArgs e)
    {
        PersonModel person1 = new PersonModel();
        PersonModel newperson = new PersonModel();
        AppUserModel user1 = new AppUserModel();
        AppUserModel newuser = new AppUserModel();

        bool flagsuccessfull = false;


        
        string resultUpload = "";
        if (m_imageBytes != null)
        {
            using (var memoryStream = new MemoryStream())
            {

                memoryStream.WriteAsync(m_imageBytes, 0, m_imageBytes.Length);

                //await m_stream.CopyToAsync(memoryStream);

                memoryStream.Position = 0;

                string imageURL = "https://khordadnet.ir/mysites/oto1/assets/person";

                using (var client = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    var streamContent = new StreamContent(memoryStream);
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    content.Add(streamContent, "file", "p0.jpg");

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


            }
        }


        activityIndicator.IsRunning = true;

        if (CheckValidation())
        {
            try
            {
                ///ذخیره اطلاعات کاربر
                user1.UserId = txtUsername.Text;
                user1.Password = txtPassword.Text;
                user1.UserRole = (byte)GlobalClass.enumUserRole.userrole_postman;
                user1.IsActive = true;
                newuser = await this.MyServiceController.AddUser(user1);

                if (newuser.UserId == null)
                {
                    activityIndicator.IsRunning = false;
                    await DisplayAlert("خطا", "شناسه کاربری موجود است", "تایید");
                }

                else
                {
                    ///ذخیره اطلاعات پیک
                    person1.FName = txtFName.Text;
                    person1.LName = txtLName.Text;
                    person1.UserId = txtUsername.Text;

                    newperson = await this.MyServiceController.AddPerson(person1);
                    activityIndicator.IsRunning = false;
                    if (newperson != null)
                    {
                        flagsuccessfull = true;
                        await DisplayAlert("پیام", "تبریک: ثبت نامت انجام شد", "قبول");
                        await Navigation.PopAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                activityIndicator.IsRunning = false;
                await DisplayAlert("خطا", ex.Message, "تایید");

            }

        }

        activityIndicator.IsRunning = false;
    }

    private bool CheckValidation()
    {
        if (txtFName.Text == "" || txtFName.Text == null)
        {
            DisplayAlert("هشدار", "نام پیک رو وارد کن", "تایید");
            return false;
        }
        if (txtUsername.Text == "" || txtUsername.Text == null)
        {
            DisplayAlert("هشدار", "شناسه کاربری رو وارد کن", "تایید");
            return false;
        }
        if (txtPassword.Text == "" || txtPassword.Text == null)
        {
            DisplayAlert("هشدار", "گذرواژه رو وارد کن", "تایید");
            return false;
        }

        return true;
    }

    private void ClearForm()
    {
        txtFName.Text = "";
        txtLName.Text = "";
        txtUsername.Text = "";
        txtPassword.Text = "";
        imgUser.Source = "user5.png";
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