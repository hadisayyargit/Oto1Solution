using CommunityToolkit.Maui;
using MauiPersianToolkit;

#if WINDOWS7_0_OR_GREATER
using CommunityToolkit.Maui.Maps;
#endif
using Microsoft.Extensions.Logging;

namespace oto1;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UsePersianUIControls();

        // تنظیمات نقشه را به این صورت یکپارچه انجام دهید
#if ANDROID || IOS
        builder.UseMauiMaps();
#endif

#if WINDOWS
   builder.UseMauiCommunityToolkitMaps("pppptertrddsxdg");  /// for windows machine  : bing 
        // در ویندوز، UseMauiMaps همان هندلر را بارگذاری می‌کند 
        // و کلید Bing باید از طریق متد ConfigureMauiHandlers یا تنظیمات پلتفرم اعمال شود.
        // اما روش استاندارد این است:
     
       // builder.UseMauiMaps(); 
#endif

        builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            fonts.AddFont("iconfont.ttf", "IconFont");
        });

        return builder.Build();
    }

}
