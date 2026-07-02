using MAUI101.Maui.Helpers;
using MAUI101.Maui.Pages;

namespace MAUI101.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AdoptionFormPage), typeof(AdoptionFormPage));

            //MainPage = ServiceProviderHelper.GetService<MainPage>();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());
#if WINDOWS
        window.Width = 500;
        window.Height = 300;
#endif
            return window;
        }
    }
}
