using CommunityToolkit.Mvvm.ComponentModel;
using UP.Models;
using UP.Services;

namespace UP.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly AppState _appState;
    public string Greeting { get; } = "Welcome to Avalonia!";

    [ObservableProperty]
    private string _greetingText = "Добро Пожаловать";

    public MainWindowViewModel(AppState appState)
    {
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
    }
}