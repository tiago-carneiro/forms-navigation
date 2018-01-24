
using Xamarin.Forms;

namespace forms_navigation
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<IDialogService, DialogService>();
            DependencyService.Register<INavigationService, NavigationService>();

            var nav = DependencyService.Get<INavigationService>();
            nav.ConfigureMap<MainViewModel, MainPage>();
            nav.ConfigureMap<SecondViewModel, SecondPage>();
            nav.ConfigureMap<LastViewModel, LastPage>();
            nav.InitializeAsync<MainViewModel>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
