
namespace oto1;


#if WINDOWS
using Microsoft.Maui.Controls.Maps;
#endif

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

//#if WINDOWS
//    Microsoft.Maui.Controls.Maps.Map.ApplicationId = "pppptertrddsxdg";
//#endif

        MainPage = new AppShell();
       // MainPage = new FlyoutPageMain();

       
    }
}
