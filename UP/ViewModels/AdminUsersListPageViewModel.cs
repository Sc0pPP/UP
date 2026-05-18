using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UP.Models;
using UP.Services;

namespace UP.ViewModels;

public partial class AdminUsersListPageViewModel:ObservableObject
{
    NavigationService _navigationService;
    AppState _appState;
    
    [ObservableProperty]
    private ObservableCollection<User> _allRL;
    
    public AdminUsersListPageViewModel(NavigationService navigationService,AppState appState)
    {
        _navigationService = navigationService;
        _appState = appState;
        AllRL=new ObservableCollection<User>(Core.db.Users.ToList());
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
        Core.db.SaveChangesAsync();
    }
    [RelayCommand]
    public void BookFrozenClick()
    {
        
    }
    [RelayCommand]
    public void ReviewFrozenClick()
    {
        
    }

}