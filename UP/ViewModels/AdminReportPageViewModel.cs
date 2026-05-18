using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using UP.Models;
using UP.Services;

namespace UP.ViewModels;

public partial class AdminReportPageViewModel:ObservableObject
{
    NavigationService _navigationService;
    AppState _appState;
    
    [ObservableProperty]
    private ObservableCollection<Report> _allRL;
    
    [ObservableProperty]
    private Report _selectedReport;
    
    [ObservableProperty]
    private int? _tempBookId;


    public AdminReportPageViewModel(NavigationService navigationService, AppState appState)
    {
        _navigationService = navigationService;
        _appState = appState;
        AllRL=new ObservableCollection<Report>(Core.db.Reports.Include(u=>u.User) .ToListAsync().Result);
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
        if (SelectedReport.BookId != null)
        {
            TempBookId = SelectedReport.BookId;
            Book TempBook = Core.db.Books.FirstOrDefault(b => b.BookId==TempBookId);
            TempBook.IsFrozen = true;
            Core.db.Books.Update(TempBook);
        }

        if (SelectedReport.ReviewId != null)
        {
            int? TempReviewID = SelectedReport.ReviewId;
            Review TempReview = Core.db.Reviews.FirstOrDefault(r => r.ReviewId==TempReviewID);
            TempReview.IsFrozen = true;
            Core.db.Reviews.Update(TempReview);
        }

        if (SelectedReport.BookId == null && SelectedReport.ReviewId == null)
        {
            int? TempUserId = SelectedReport.ReportedUserId;
            User TempUser = Core.db.Users.FirstOrDefault(u => u.UserId==TempUserId);
            TempUser.IsFrozen = true;
            TempUser.FrozenReasons = null;
            Core.db.Users.Update(TempUser);
        }
        SelectedReport.IsChecked=true;
        Core.db.Reports.Update(SelectedReport);
        Core.db.SaveChanges();
    }
    
    
    [RelayCommand]
    public void DisApproveClick()
    {
        
        Core.db.Reports.Remove(SelectedReport);
        Core.db.SaveChanges();
    }
}