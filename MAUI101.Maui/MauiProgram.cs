using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using MAUI101.Maui.Services;
using MAUI101.Maui.Pages;
using MAUI101.Maui.Helpers;
using MAUI101.Maui.ViewModels;
using MAUI101.Maui.Repositories;
using Microsoft.AspNetCore.Identity;
using MAUI101.Maui.Models;

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

#if DEBUG
            using var stream = a.GetManifestResourceStream($"MAUI101.Maui.Resources.Raw.config.local.json");
#else
            using var stream = a.GetManifestResourceStream($"MAUI101.Maui.Resources.Raw.config.json");
#endif
 
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();
            builder.Configuration.AddConfiguration(config);


            builder.Services.AddSingleton<IDbConnectionProvider, DbConnectionProvider>();
            builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddSingleton<IPasswordHelper, PasswordHelper>();
            builder.Services.AddSingleton<IAdoptionFormRepository, AdoptionFormRepository>();
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IAdoptionFormService, AdoptionFormService>();
            builder.Services.AddSingleton<IRestService, RestService>();
		    builder.Services.AddSingleton<IPetService, PetService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            
            builder.Services.AddTransient<AdoptionFormViewModel>();
            builder.Services.AddTransient<AdoptionListViewModel>();
            builder.Services.AddTransient<LoginViewModel>();

            
		    builder.Services.AddSingleton<AboutPage>();
		    builder.Services.AddSingleton<AdoptionListPage>();
            builder.Services.AddSingleton<AdoptionFormListPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<AdoptionFormPage>();


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
