using System.Windows.Input;
using Xamarin.Forms;

namespace forms_navigation
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand ShowSecondPageCommand => new Command(ExecuteShowSecondPageCommand);
        public ICommand ShowSecondPageParmsCommand => new Command(ExecuteShowSecondPageParmsCommand);

        string _textParameter;
        public string TextParameter
        {
            get => _textParameter;
            set => SetProperty(ref _textParameter, value);
        }


        public MainViewModel() : base("MainPage") { }

        async void ExecuteShowSecondPageCommand()
            => await NavigationService.NavigateToAsync<SecondViewModel>();

        async void ExecuteShowSecondPageParmsCommand()
        {
            if (string.IsNullOrEmpty(TextParameter))
            {
                await DialogService.AlertAsync("Atenção", "O campo deve ser preenchido", "Voltar");
                return;
            }
            await NavigationService.NavigateToAsync<SecondViewModel>(TextParameter);
        }
    }
}
