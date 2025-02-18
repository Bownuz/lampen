using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using HueApp_Fedde_en_Siem.Onion.Presentation.ViewModels;
using HueApp_Fedde_en_Siem.Onion.Application;
using HueApp_Fedde_en_Siem.Onion.Domain;

namespace HueApp_Fedde_en_Siem
{
    public static class MauiProgram {
        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<HueConfiguration>();
            builder.Services.AddSingleton<IHueService, HueAPIService>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddTransient<PopupTestViewModel>();

            return builder.Build();
        }
    }
}
