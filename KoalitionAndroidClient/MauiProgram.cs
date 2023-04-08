using System.Net.Http.Headers;
using KoalitionAndroidClient.Services;
using KoalitionAndroidClient.ViewModels.Menu;
using KoalitionAndroidClient.ViewModels.Startup;
using KoalitionAndroidClient.Views.Menu;
using KoalitionAndroidClient.Views.Startup;
using Microsoft.Extensions.DependencyInjection;

namespace KoalitionAndroidClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ILoginService, LoginService>();

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<MenuPage>();


            builder.Services.AddSingleton<LoginPageViewModel>();
            builder.Services.AddSingleton<MenuPageViewModel>();

            return builder.Build();
        }
    }
}
