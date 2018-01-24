using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace forms_navigation
{
    public class SecondViewModel : ViewModelBase
    {
        public ICommand GoBackCommand => new Command(ExecuteGoBackCommand);
        public ICommand ShowLastPageCommand => new Command(ExecuteShowLastPageCommand);

        string _textParameter;
        public string TextParameter
        {
            get => _textParameter;
            set => SetProperty(ref _textParameter, value);
        }

        public SecondViewModel() : base("SecondPage") { }

        public override async Task InitializeAsync(object navigationData) 
            => TextParameter = navigationData?.ToString();

        async void ExecuteGoBackCommand()
            => await NavigationService.NavigateBackAsync();

        async void ExecuteShowLastPageCommand()
           => await NavigationService.NavigateToAsync<LastViewModel>();
    }
}
