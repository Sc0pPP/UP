using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UP.Models;
using UP.Services;

namespace UP.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly AppState _appState;
    private readonly NavigationService _navigationService;
    public string Greeting { get; } = "Welcome to Avalonia!";

    [ObservableProperty]
    private bool _isAuthor = false;
    
    [ObservableProperty]
    private bool _isAdmin= false;
    
    [ObservableProperty]
    private bool _isFrozen= false;

    
    [ObservableProperty]
    private string _greetingText = "Добро Пожаловать";

    public MainWindowViewModel(AppState appState, NavigationService navigationService)
    {
        _navigationService = navigationService;
        _appState = appState;
        _appState.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(AppState.CurrentUser))
                UpdateGreeting();
        };
    }
    private void UpdateGreeting()
    {
        GreetingText = _appState.CurrentUser == null
            ? "Добро Пожаловать"
            : "Добро Пожаловать, " + _appState.CurrentUser.FirstName;
        if (_appState.CurrentUser.Role == 1)
        {
            Console.WriteLine(_appState.CurrentUser.Role);
            IsAuthor = false;
            IsAdmin=false;
            IsFrozen=_appState.CurrentUser.IsFrozen;
        }

        if (_appState.CurrentUser.Role == 2)
        {
            Console.WriteLine(_appState.CurrentUser.Role);
            IsAuthor = true;
            IsAdmin = false;
            IsFrozen=_appState.CurrentUser.IsFrozen;
        }
        if (_appState.CurrentUser.Role == 3)
        {
            Console.WriteLine(_appState.CurrentUser.Role);
            IsAuthor = false;
            IsAdmin = true;
            IsFrozen=false;
        }
    }

    [RelayCommand]
    public void GoToCatalog()
    {
        _navigationService.ReplaceToAsync<FirstPageViewModel>();
    }

    [RelayCommand]
    public void GoToLists()
    {
        _navigationService.ReplaceToAsync<ListsPageViewModel>();
    }

    [RelayCommand]
    public void GoToProfile()
    {
        _navigationService.ReplaceToAsync<ProfilePageViewModel>();
    }

    [RelayCommand]
    public void GoToAuthorPage()
    {
        
    }

    [RelayCommand]
    public void GoToAdmin()
    {
        
    }

    [RelayCommand]
    public void GoToFreezeWarning()
    {
        
    }
}