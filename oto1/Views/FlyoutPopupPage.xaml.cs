namespace oto1;

using Microsoft.Maui.Media;
using oto1.Models;
using System.Collections.ObjectModel;

public partial class FlyoutPopupPage : ContentPage
{
    ObservableCollection<FlyoutPageItemModel> objFlyoutPageItems;

    public FlyoutPopupPage()
	{
		InitializeComponent();
        objFlyoutPageItems = new ObservableCollection<FlyoutPageItemModel>();
        objFlyoutPageItems.Add(new FlyoutPageItemModel() {Title="خانه",IconSource="home1.png" ,TargetType= typeof(MainPage) , MenuName="mnuHome"});
        objFlyoutPageItems.Add(new FlyoutPageItemModel() { Title = "درباره...", IconSource = "logo28.png", TargetType = typeof(MyProfile), MenuName = "mnuAbout" });
        objFlyoutPageItems.Add(new FlyoutPageItemModel() { Title = "بروزرسانی", IconSource = "recurring1_48.png", TargetType = typeof(MyProfile) , MenuName = "mnuUpdate" });
        objFlyoutPageItems.Add(new FlyoutPageItemModel() { Title = "خروج", IconSource = "user4.png", TargetType = typeof(SignInPage) , MenuName = "mnuLogOff"  });
        collectionViewMenu.ItemsSource = objFlyoutPageItems;

    }

    private void OnLabelTapped(object sender, EventArgs e)
    {
        //var label = (Label)sender;
        //var selecteditem = (FlyoutPageItemModel)label.BindingContext;
        //OnMenuClicked(selecteditem.MenuName);
    }

    private void OnMenuClicked(string menuname)
    {

        // Handle the click event here
        if (menuname == "mnuAbout")
        {
            DisplayAlert("", "گروه پردازشگران خرداد", "ok");
        }
        if (menuname == "mnuHome")
        {
            //Shell.Current.GoToAsync("//MapPage");

             //TabbedPage bb = (TabbedPage)this.Parent;

           // TabbedPage aa = new MainPage();
          //  aa.CurrentPage = aa.Children[0];


        }
    }

}