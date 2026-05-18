using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UP.Models;
using UP.Services;

namespace UP.ViewModels;

public partial class AdminAuthorBidPageViewModel:ObservableObject
{
    NavigationService _navigationService;
    AppState _appState;
    
    
    [ObservableProperty]
    private ObservableCollection<RoleBid> _allRL;

    [ObservableProperty]
    private RoleBid _selectedBid;

    public AdminAuthorBidPageViewModel(NavigationService navigationService, AppState appState)
    {
        _navigationService = navigationService;
        _appState = appState;
        _allRL=new ObservableCollection<RoleBid>(Core.db.RoleBids.ToList());
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
    public void ApproveClick()
    {
        int TempUserId = SelectedBid.UserId;
        User user=Core.db.Users.FirstOrDefault(u=>u.UserId==TempUserId);
        user.Role = 2;
        SelectedBid.IsChecked=true;
        Core.db.RoleBids.Update(SelectedBid);
        Core.db.Users.Update(user);
        Core.db.SaveChanges();
    }

    [RelayCommand]
    public void DisApproveClick()
    {
        
    }
    

}