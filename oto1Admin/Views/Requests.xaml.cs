using oto1;
using oto1.Models;
using oto1.Services;

namespace oto1;

public partial class Requests : ContentPage
{
    //public ServiceController MyServiceController { get; set; }



    public Requests()
    {
        InitializeComponent();
    }

    private async void RefreshForm()
    {



    }

    private void toolbarItemOrder_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Orders());

    }
}