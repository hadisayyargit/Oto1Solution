using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using MauiPersianToolkit;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using oto1.Models;
using oto1.Services;
using System.Diagnostics;
using TabbedPage = Microsoft.Maui.Controls.TabbedPage;

namespace oto1;
public partial class MainPage : TabbedPage
{

    public class UserToolbarMessage : ValueChangedMessage<string>
    {
        public UserToolbarMessage(string value) : base(value) { }
    }


    public MainPage()
	{
		InitializeComponent();
          On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().DisableSwipePaging();

        //Task.Delay(2000);

        //MessagingCenter.Subscribe<myRequests, string>(this, "UpdateToolbarItem", (sender, item) =>
        //{
        //    UpdateToolbarItem(item);

        //});

        ///<HADI 14050503></HADI>

        //MessagingCenter.Subscribe<SignInPage, string>(this, "updateUserToolbarItem", (sender, item) =>
        //{

        //    updateUserToolbarItem(item);

        //});
        //Navigation.PushAsync(new SignInPage());

    

        // در سازنده کلاس یا جایی که اشتراک ایجاد می‌شد:
        WeakReferenceMessenger.Default.Register<string>(this, (r, m) =>
        {
            if (m == "updateUserToolbarItem")
            {
                updateUserToolbarItem(m);
            }
        });



        //sayhello();

    }


    private async void sayhello()
    {
        ServiceController MyServiceController = new ServiceController();
        string s = "";

        //s = await MyServiceController.SayHello();

        //CustomerModel mycustomer = await MyServiceController.GetCustomerByUserId("madjid", "test");
    }

    private void UpdateToolbarItem(string s)
    {

        toolbaritemBasket.Text = s;
    }

    private async void updateUserToolbarItem(string s)
    {
        //toolbaritemUser.IconImageSource = GlobalClass.m_UserThumbnailPhotoString;
  
        toolbaritemUser.IconImageSource = "user5.png";

        if (GlobalClass.m_UserId != "")
        {
            string fileUrl = "https://khordadnet.ir/mysites/oto1/assets/customer/c" + GlobalClass.m_CustomerId.ToString() + ".png";

            bool fileExists = await NetClass.CheckNetFileExists(fileUrl);
            if (fileExists) toolbaritemUser.IconImageSource = fileUrl;
        }
    }

    private async void toolbaritemUser_Clicked(object sender, EventArgs e)
    {
        if (GlobalClass.m_UserId == "" || GlobalClass.m_UserId == null)
        {
            Navigation.PushAsync(new SignInPage());

        }
        else
        {
            bool answer =  await DisplayAlert("هشدار", "از حساب خود خارج می‌شوی؟", "آره", "نه");

            //string action =  await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook","LinkedIn");

            //string result = await DisplayPromptAsync("Question 1", "What's your name?");

            if (answer == true)
            {
                toolbaritemLogoff_Clicked(this, null);
            }
          

        }
    }

    private void toolbaritemBasket_Clicked(object sender, EventArgs e)
    {
        CurrentPage = this.Children[0];
        Navigation.PushAsync(new Baskets());
    }

    private void toolbaritemLogoff_Clicked(object sender, EventArgs e)
    {
        //var answer = DisplayAlert("هشدار", "از حساب کاربری بیرون می‌روی؟", "بله", "خیر");
        //if ( answer.ToBool() )
        //{
            GlobalClass.m_UserId = "";
            toolbaritemUser.IconImageSource = "user1.png";
      //  }
        
    }

    private void toolbaritemSmartChat_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SmartChat());
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        WeakReferenceMessenger.Default.Unregister<UserToolbarMessage>(this);
    }

}

