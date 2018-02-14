
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

            NavigationService.ConfigureMap<MainViewModel, MainPage>();
            NavigationService.ConfigureMap<SecondViewModel, SecondPage>();
            NavigationService.ConfigureMap<LastViewModel, LastPage>();

            Initialize();
        }

        async void Initialize()
        {
            var navigationService = DependencyService.Get<INavigationService>();
            await navigationService.NavigateToAsync<MainViewModel>();
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
