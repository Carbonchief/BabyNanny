using BabyNanny.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Networking;



namespace BabyNanny
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
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddTelerikBlazor();
            builder.Services.AddSingleton(c => Connectivity.Current);
            builder.Services.AddSingleton<IDialogService, DialogService>();
            builder.Services.AddHttpClient("api", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5180");
            });
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif


            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "BabyNanny.db");

            builder.Services.AddSingleton(s =>
                ActivatorUtilities.CreateInstance<BabyNannyRepository>(s,
                    dbPath,
                    s.GetRequiredService<IHttpClientFactory>().CreateClient("api")));
            builder.Services.AddSingleton<ChildState>();

            return builder.Build();
        }
    }
}
