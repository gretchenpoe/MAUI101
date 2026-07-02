using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using MAUI101.Maui.Services;
using MAUI101.Maui.Pages;
using MAUI101.Maui.Helpers;
using MAUI101.Maui.ViewModels;

namespace MAUI101.Maui
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

            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream($"MAUI101.Maui.Resources.Raw.config.json");
            var names = a.GetManifestResourceNames();
            foreach( var name in names )
            {
                Console.WriteLine( name );
            }
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();
            builder.Configuration.AddConfiguration(config);


            builder.Services.AddSingleton<IRestService, RestService>();
		    builder.Services.AddSingleton<IPetService, PetService>();

            
		    builder.Services.AddSingleton<AboutPage>();
		    builder.Services.AddSingleton<AdoptionListPage>();
            builder.Services.AddSingleton<AdoptionFormsPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<AdoptionDetailsPage>();
            builder.Services.AddTransient<AdoptionDetailsViewModel>();


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
