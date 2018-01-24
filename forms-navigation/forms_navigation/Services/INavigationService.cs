using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace forms_navigation
{
    public interface INavigationService
    {
        void ConfigureMap<TViewModel, TPage>() where TViewModel : ViewModelBase where TPage : Page;
        Task InitializeAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;
        Task NavigateBackAsync();
        Task NavigateAndClearBackStackAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase;
    }

    public class NavigationService : INavigationService
    {
        static readonly Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();
        protected Application CurrentApplication => Application.Current;

        public void ConfigureMap<TViewModel, TPage>() where TViewModel : ViewModelBase
                                                          where TPage : Page
            => _mappings.Add(typeof(TViewModel), typeof(TPage));

        public async Task InitializeAsync<TViewModel>() where TViewModel : ViewModelBase
            => await NavigateToAsync<TViewModel>(null, false);

        public async Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
            => await NavigateToAsync<TViewModel>(null, false);

        public async Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
            => await NavigateToAsync<TViewModel>(parameter, false);

        public async Task NavigateAndClearBackStackAsync<TViewModel>(object parameter = null) where TViewModel : ViewModelBase
            => await NavigateToAsync<TViewModel>(parameter, true);

        public async Task NavigateBackAsync()
             => await CurrentApplication?.MainPage?.Navigation.PopAsync();

        async Task NavigateToAsync<TViewModel>(object parameter, bool cleanBackStack) where TViewModel : ViewModelBase
        {
            var page = CreateAndBindPage(typeof(TViewModel), parameter);

            var navigationPage = CurrentApplication.MainPage as NavigationPage;

            if (navigationPage == null)
                CurrentApplication.MainPage = new NavigationPage(page);
            else
                await navigationPage.PushAsync(page);

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);

            if (navigationPage != null && cleanBackStack && navigationPage.Navigation.NavigationStack.Count > 0)
            {
                var existingPages = navigationPage.Navigation.NavigationStack.ToList();

                foreach (var existingPage in existingPages)
                {
                    if (existingPage != page)
                        navigationPage.Navigation.RemovePage(existingPage);
                }
            }
        }

        Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");

            return _mappings[viewModelType];
        }

        Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
                throw new Exception($"Mapping type for {viewModelType} is not a page");

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = Activator.CreateInstance(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;
            return page;
        }
    }
}
