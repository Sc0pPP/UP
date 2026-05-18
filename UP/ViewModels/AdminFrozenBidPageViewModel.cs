using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UP.Models;
using UP.Services;

namespace UP.ViewModels;

public partial class AdminFrozenBidPageViewModel:ObservableObject
{
    NavigationService _navigationService;
    AppState _appState;
    
    [ObservableProperty]
    private ObservableCollection<FrozenBid> _allRL;
    
    [ObservableProperty]
    private FrozenBid _selectedBid;

    [ObservableProperty] 
    public int? _tempBookId;

    public AdminFrozenBidPageViewModel(NavigationService navigationService, AppState appState)
    {
        _navigationService = navigationService;
        _appState = appState;
        AllRL=new ObservableCollection<FrozenBid>(Core.db.FrozenBids.ToList());
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
        if (SelectedBid.BookId != null)
        {
             TempBookId = SelectedBid.BookId;
             Book TempBook = Core.db.Books.FirstOrDefault(b => b.BookId==TempBookId);
             TempBook.IsFrozen = false;
             Core.db.Books.Update(TempBook);
        }

        if (SelectedBid.ReviewId != null)
        {
            int? TempReviewID = SelectedBid.ReviewId;
            Review TempReview = Core.db.Reviews.FirstOrDefault(r => r.ReviewId==TempReviewID);
            TempReview.IsFrozen = false;
            Core.db.Reviews.Update(TempReview);
        }

        if (SelectedBid.BookId == null && SelectedBid.ReviewId == null)
        {
            int? TempUserId = SelectedBid.UserId;
            User TempUser = Core.db.Users.FirstOrDefault(u => u.UserId==TempUserId);
            TempUser.IsFrozen = false;
            TempUser.FrozenReasons = null;
            Core.db.Users.Update(TempUser);
        }
        Core.db.SaveChanges();
    }

    
    [RelayCommand]
    public void DisApproveClick()
    {
        
        Core.db.FrozenBids.Remove(_selectedBid);
        Core.db.SaveChanges();
    }
}