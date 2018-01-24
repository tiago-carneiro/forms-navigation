using System.Windows.Input;
using Xamarin.Forms;

namespace forms_navigation
{
    public class LastViewModel : ViewModelBase
    {
        public ICommand ReturnFirstCommand => new Command(ExecuteReturnFirstCommand);
        
        public LastViewModel() : base("Last Page") { }        

        async void ExecuteReturnFirstCommand()
            => await NavigationService.NavigateAndClearBackStackAsync<MainViewModel>();
    }
}
