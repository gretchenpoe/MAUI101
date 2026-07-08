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
        IServiceProvider _services;
        public App(IServiceProvider services)
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AdoptionFormPage), typeof(AdoptionFormPage));
            _services = services;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = new Window(_services.GetRequiredService<LoginPage>()); // Navigate to LoginPage if not logged in


#if WINDOWS
        window.Width = 500;
        window.Height = 300;
#endif
            return window;
        }
    }
}
