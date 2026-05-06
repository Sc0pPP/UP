using CommunityToolkit.Mvvm.ComponentModel;
using UP.Models;

namespace UP.Services;

public partial class AppState:ObservableObject
{
    [ObservableProperty]
    private Book _currentBook;
    [ObservableProperty]
    private User? _currentUser;
}