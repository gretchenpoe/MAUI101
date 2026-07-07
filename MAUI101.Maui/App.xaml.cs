using MAUI101.Maui.Models;
using MAUI101.Maui.Pages;
using MAUI101.Maui.Repositories;
using MAUI101.Maui.Services;
using MAUI101.Maui.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace MAUI101.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AdoptionFormPage), typeof(AdoptionFormPage));
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window;
            // 1. Check if user is logged in (using Microsoft.Maui.Storage Preferences as an example)
            //bool IsUserLoggedIn = Preferences.Default.Get("IsLoggedIn", false);
             //if (!IsUserLoggedIn)
            window = new Window(new LoginPage(new LoginViewModel(new UserService(new UserRepository(), new PasswordHelper(new PasswordHasher<User>()))))); // Navigate to LoginPage if not logged in


#if WINDOWS
        window.Width = 500;
        window.Height = 300;
#endif
            return window;
        }
    }
}
