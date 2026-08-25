using oto1.Models;

namespace oto1;

public partial class TestPage : ContentPage
{

    public TestPage()
    {

        InitializeComponent();
  
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet("ActionSheet: Send to?", "Cancel", null, "Email", "Twitter", "Facebook");

        string result = await DisplayPromptAsync("Question 1", "What's your name?");
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
       
    }
}