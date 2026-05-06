using CommunityToolkit.Mvvm.ComponentModel;
using UP.Services;

namespace UP.ViewModels;

public partial class CurrentBookPageViewModel:ObservableObject
{
    private readonly AppState _appState;
    private readonly NavigationService _navigationService;
    
    [ObservableProperty] 
    private string _username;

    public CurrentBookPageViewModel(NavigationService navigationService,AppState appState)
    {
        _appState = appState;
        _navigationService = navigationService;
        Username = _appState.CurrentUser.fio;
    }
    
}