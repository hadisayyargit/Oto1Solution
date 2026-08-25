using CommunityToolkit.Maui;
using MauiPersianToolkit;

#if WINDOWS7_0_OR_GREATER
using CommunityToolkit.Maui.Maps;
#endif
using Microsoft.Extensions.Logging;

namespace oto1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UsePersianUIControls()
#if ANDROID || IOS
                .UseMauiMaps()   ///for android        : google map        
#endif

#if WINDOWS7_0_OR_GREATER
                   .UseMauiCommunityToolkitMaps("pppptertrddsxdg")  /// for windows machine  : bing 

#endif

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("iconfont.ttf", "IconFont");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
