using CommunityToolkit.Mvvm.ComponentModel;
using UP.Models;

namespace UP.Services;

public partial class AppState:ObservableObject
{
    public static AppState Instance { get; } = new();
    
    [ObservableProperty]
    private Book _currentBook;
}