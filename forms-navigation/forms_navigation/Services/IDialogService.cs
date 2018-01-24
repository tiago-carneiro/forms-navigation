using System.Threading.Tasks;
using Xamarin.Forms;

namespace forms_navigation
{
    public interface IDialogService
    {
        Task AlertAsync(string title, string message, string cancel);
    }

    public class DialogService : IDialogService
    {
        public async Task AlertAsync(string title, string message, string cancel)
            => await Application.Current.MainPage.DisplayAlert(title, message, cancel);        
    }
}
