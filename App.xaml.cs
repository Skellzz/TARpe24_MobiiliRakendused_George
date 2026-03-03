using Microsoft.Extensions.DependencyInjection;

namespace Naidis_TARpe24
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AppShell());
        }

       // protected override Window CreateWindow(IActivationState? activationState)
        //{
          //  return new Window(new AppShell());
        //}
    }
}