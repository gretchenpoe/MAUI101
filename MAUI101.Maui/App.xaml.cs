using MAUI101.Maui.Helpers;

namespace MAUI101.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));

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
