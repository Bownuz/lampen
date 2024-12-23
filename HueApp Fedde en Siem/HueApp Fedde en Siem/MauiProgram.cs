﻿using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace HueApp_Fedde_en_Siem {
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
            //builder.Services.AddSingleton<IHueService, HueService>();
            //builder.Services.AddTransient<LampViewModel>();
            //builder.Services.AddTransient<MainPage>();

            return builder.Build();
        }
    }
}
