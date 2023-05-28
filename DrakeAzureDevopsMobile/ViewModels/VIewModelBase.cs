using DrakeAzureDevopsMobile.Helpers;
using DrakeAzureDevopsMobile.Services.Interfaces;

namespace DrakeAzureDevopsMobile.ViewModels
{
    public class VIewModelBase : Bindablebase
    {

        public readonly INavigationService NavigationService;

        public VIewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
        }

        
        

        public Task ShowAlertAsync(string title, string message, string cancel = "OK")
        {
            return Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }

        

        public virtual Task OnNavigatingTo(Dictionary<string, object> parameter)
           => Task.CompletedTask;

        public virtual Task OnNavigatedFrom(bool isForwardNavigation)
            => Task.CompletedTask;

        public virtual Task OnNavigatedTo()
            => Task.CompletedTask;
    }
}
