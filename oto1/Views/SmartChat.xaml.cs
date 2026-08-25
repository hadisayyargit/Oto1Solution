namespace oto1;

public partial class SmartChat : ContentPage
{
    public SmartChat()
    {
        InitializeComponent();

       // Animate();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Animate();
    }
    private async void Animate()
    {
        await Task.Delay( 100 );


        ///Translate
        var rightDist = (DeviceDisplay.MainDisplayInfo.Width - BotImg.X) / DeviceDisplay.MainDisplayInfo.Density;

        await Task.WhenAll(
            BotImg.TranslateTo(rightDist, 0, 1000, easing: Easing.CubicInOut),
            BotImg.ScaleTo(0.4, 1000, easing: Easing.CubicInOut)
        );

        BotImg.TranslationX = rightDist * -1;

        await Task.WhenAll(
            BotImg.TranslateTo(0, 0, 1000, easing: Easing.CubicInOut),
            BotImg.ScaleTo(1, 1000, easing: Easing.CubicInOut)
        );

        BotImg.TranslationX = 0;
        BotImg.Scale = 1;


        ///Fade
        await Task.Delay(200);
        await BotImg.FadeTo(0);
        await Task.Delay(200);
        await BotImg.FadeTo(1);


        ///Rotate
        await Task.Delay(200);
        await Task.WhenAny<bool>(
            BotImg.RotateTo(360, 500, Easing.CubicInOut),
   BotImg.TranslateTo(0, -50, 250, Easing.CubicInOut)
        );
        await BotImg.TranslateTo(0, 0, 250, Easing.CubicInOut);
        BotImg.Rotation = 0;



        ///Scale
        await Task.Delay(200);
        await BotImg.ScaleTo(0, easing: Easing.SpringIn);
        await Task.Delay(200);
        await BotImg.ScaleTo(1, easing: Easing.SpringOut);
    }

}