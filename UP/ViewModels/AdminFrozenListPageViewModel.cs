using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UP.Models;
using UP.Services;

namespace UP.ViewModels;

public partial class AdminFrozenListPageViewModel:ObservableObject
{
    NavigationService _navigationService;
    AppState _appState;

    
    [ObservableProperty]
    private ObservableCollection<Book> _bookIS;

    [ObservableProperty]
    private ObservableCollection<User> _userIS;
    
    [ObservableProperty]
    private ObservableCollection<Review> _reviewIS;
    
    [ObservableProperty]
    private bool _userVisible=false;
    
    [ObservableProperty]
    private bool _bookVisible=false;
    
    [ObservableProperty]
    private bool _reviewVisible=false;
    

    public AdminFrozenListPageViewModel(NavigationService navigationService, AppState appState)
    {
        _navigationService = navigationService;
        _appState = appState;
        BookIS =new ObservableCollection<Book>( Core.db.Books.Where(b => b.IsFrozen == true).ToList());
        UserIS =new ObservableCollection<User>( Core.db.Users.Where(b => b.IsFrozen == true).ToList());
        ReviewIS =new ObservableCollection<Review>( Core.db.Reviews.Where(b => b.IsFrozen == true).ToList());


    }
    
    
    [RelayCommand]
    public void AuthorBidClick()
    {
        _navigationService.ReplaceToAsync<AdminAuthorBidPageViewModel>();
    }
    [RelayCommand]
    public void FrozenBidClick()
    {
        _navigationService.ReplaceToAsync<AdminFrozenBidPageViewModel>();

    }
    [RelayCommand]
    public void FrozenListClick()
    {
        _navigationService.ReplaceToAsync<AdminFrozenListPageViewModel>();

    }
    [RelayCommand]
    public void ReportsClick()
    {
        _navigationService.ReplaceToAsync<AdminReportPageViewModel>();
    }
    [RelayCommand]
    public void UsersListClick()
    {
        _navigationService.ReplaceToAsync<AdminUsersListPageViewModel>();

    }

    [RelayCommand]
    public void UserFrozenClick()
    {
        UserVisible = true;
        BookVisible = false;
        ReviewVisible = false;
    }
    [RelayCommand]
    public void BookFrozenClick()
    {
        BookVisible = true;
        ReviewVisible = false;
        UserVisible = false;
    }
    [RelayCommand]
    public void ReviewFrozenClick()
    {
        ReviewVisible = true;
        BookVisible = false;
        UserVisible = false;
    }
}