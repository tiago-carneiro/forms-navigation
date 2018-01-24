using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace forms_navigation
{
    public class ViewModelBase : BindingObject
    {
        protected INavigationService NavigationService 
            => DependencyService.Get<INavigationService>();

        protected IDialogService DialogService
            => DependencyService.Get<IDialogService>();

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ViewModelBase(string title) => Title = title;

        public virtual async Task InitializeAsync(object navigationData)
        {
            if (navigationData == null)
                await InitializeAsync();
        }

        public virtual async Task InitializeAsync()
            => await Task.FromResult(true);
    }

    public class BindingObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void SetProperty<TValue>(ref TValue prop, TValue value, [CallerMemberName] string propertyName = "")
        {
            prop = value;
            RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


