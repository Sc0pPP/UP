using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using UP.Models;
using UP.Services;

namespace UP.ViewModels;

public partial class ProfilePageViewModel:ObservableObject
{
    private readonly NavigationService _navigationService;
    
    private readonly AppState _appState;

    [ObservableProperty]
    private string _userName;
    
    [ObservableProperty]
    private string _userLogin;
    
    [ObservableProperty]
    private string _mail;
    
    [ObservableProperty]
    private string _phone;
    
    [ObservableProperty]
    private ICollection<Review> _review;

    [ObservableProperty] 
    private string _bookName;

    [ObservableProperty] 
    private bool _authorButton = false;

    [ObservableProperty] 
    private string _freezeText;

    [ObservableProperty] 
    private bool _freezeTextVisible;
    

    public ProfilePageViewModel(NavigationService navigationService, AppState appState)
    {
        _navigationService = navigationService;
        _appState = appState;
        UserName = _appState.CurrentUser.FirstName;
        UserLogin = _appState.CurrentUser.Login;
        Mail=_appState.CurrentUser.Email;
        Phone=_appState.CurrentUser.PhoneNumber;
        Review=Core.db.Reviews
            .Where(u=>u.UserId==_appState.CurrentUser.UserId).ToList();
        if (_appState.CurrentUser.Role == 1)
        {
            AuthorButton = true;
        }

        if (_appState.CurrentUser.IsFrozen == true)
        {
            FreezeTextVisible = true;
            FreezeText = _appState.CurrentUser.FrozenReasons;
        }
        else
        {
            FreezeTextVisible = false;
        }
        
    }
    
    [RelayCommand]
    public void BidFreezeClick()
    {
        _appState.ReportType= BidTypes.FreezeUserReport;
        _navigationService.ReplaceToAsync<BidPageViewModel>();
        
    }

    [RelayCommand]
    public void BidAuthorClick()
    {
        _appState.ReportType= BidTypes.AuthorBid;
        _navigationService.ReplaceToAsync<BidPageViewModel>();
    }
}