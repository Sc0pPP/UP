using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using UP.Models;
using UP.Services;

namespace UP.ViewModels;

public partial class CurrentBookPageViewModel:ObservableObject
{
    private readonly AppState _appState;
    private readonly NavigationService _navigationService;

    [ObservableProperty] 
    private string _name;
    
    [ObservableProperty]
    private string _author;
    
    [ObservableProperty]
    private ICollection<Review> _review;
    
    [ObservableProperty]
    private ICollection<BookGenre> _bookGenres;

    [ObservableProperty] private Book _currentBook;
    
    [ObservableProperty] 
    private string _description;

    [ObservableProperty] 
    private string _longText;

    [ObservableProperty] 
    private bool _visibleButton;
    
    [ObservableProperty]
    private Review _selectedReview;

    [ObservableProperty] 
    private bool _visibleButtonForStandartUs;

    [ObservableProperty] 
    private string _reviewTextBox;

    [ObservableProperty]
    private int _rating = 5;
    
    public CurrentBookPageViewModel(NavigationService navigationService,AppState appState)
    {
        
       
        _appState = appState;
        _navigationService = navigationService;
        _name = _appState.CurrentBook.Name;
        _author = Core.db.Users.FirstOrDefault(u => u.UserId == _appState.CurrentBook.Author).fio;
        _bookGenres = _appState.CurrentBook.BookGenres;
        Review=Core.db.Reviews.Where(u=>u.BookId==_appState.CurrentBook.BookId && u.IsFrozen==false).ToList();
        _description = _appState.CurrentBook.Description;
        _longText = _appState.CurrentBook.Content;
        if (_appState.CurrentUser.Role == 3)
        {
            VisibleButton = true;
        }
       if(_appState.CurrentUser.Role==1)
        {
            VisibleButton = false;
            _visibleButtonForStandartUs = true;
        }
        if(_appState.CurrentUser.Role==2)
        {
            VisibleButton = false;
        }
        
    }

    [RelayCommand]
    public void ClickReview(Review review)
    {
        review.IsFrozen=true;
        Core.db.SaveChanges();
    }

    [RelayCommand]
    public void FrozeBookClick()
    {
        Book TempBook=Core.db.Books.FirstOrDefault(b => b.BookId == _appState.CurrentBook.BookId);
        TempBook.IsFrozen=true;
        Core.db.SaveChanges();
    }

    [RelayCommand]
    public void BidBookClick()
    {
        _appState.ReportType = BidTypes.BookReport;
        _navigationService.ReplaceToAsync<BidPageViewModel>();
    }
    [RelayCommand]
    public void BidReviewClick()
    {
        _appState.ReportType = BidTypes.ReviewReport;
        _navigationService.ReplaceToAsync<BidPageViewModel>();
    }
    
    [RelayCommand]
    public void BidAuthorClick()
    {
        _appState.ReportType = BidTypes.AuthorReport;
        _navigationService.ReplaceToAsync<BidPageViewModel>();
    }
    
    partial void OnSelectedReviewChanged(Review? value)
    {
        if (value != null)
            _appState.CurrentReview = value;
    }

    [RelayCommand]
    public void AddReviewClick()
    {
        Review rev=new Review()
        {
            UserId = _appState.CurrentUser.UserId,
            ReviewText = ReviewTextBox,
            BookId = _appState.CurrentBook.BookId,
            IsFrozen=false,
            Rating = Rating
        };
        Core.db.Reviews.Add(rev);
        Core.db.SaveChanges();
        MessageBoxService.MessageBoxShow("Ваш отзыв добавлен");
    }
    
}