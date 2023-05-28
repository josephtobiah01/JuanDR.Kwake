using DrakeAzureDevopsMobile.Interfaces;
using DrakeAzureDevopsMobile.ViewModels.Models.Global;

namespace DrakeAzureDevopsMobile.Views;

public partial class BoardsPage : ContentPage
{
    private IAzureLoginPageViewModel _azureLoginPageViewModel;
    private readonly IDispatcher _dispatcher;
    private const int defaultTimeSpanForStart = 2;


    public BoardsPage(
        IAzureLoginPageViewModel azureLoginPageViewModel,
        IDispatcher dispatcher)
	{
		InitializeComponent();
        _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        _azureLoginPageViewModel = azureLoginPageViewModel ?? throw new ArgumentNullException(nameof(azureLoginPageViewModel));
    }

    private Task ShowOkMessage(string title, string message, string cancel = "OK")
    {

        App.Current.MainPage.DisplayAlert(title, message, cancel);
        return Task.CompletedTask;

    }
    
}