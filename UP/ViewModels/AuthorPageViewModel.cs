using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UP.Models;
using UP.Services;

namespace UP.ViewModels;

public partial class AuthorPageViewModel:ObservableObject
{
    private readonly NavigationService _navigationService;
    private readonly AppState _appState;
    private readonly MessageBoxService _messageBoxService;

    [ObservableProperty]
    private string _userFio;

    [ObservableProperty] 
    private string _mail;

    [ObservableProperty] 
    private string _phone;

    [ObservableProperty]
    private ObservableCollection<Book> _booksIS;
    
    [ObservableProperty]
    private ObservableCollection<Book> _frozeBooksIS;

    [ObservableProperty] 
    private Book _selectedBook;
    
    [ObservableProperty] 
    private Book _selectedFreezeBook;


    public AuthorPageViewModel(NavigationService navigationService,AppState appState)
    {
        _navigationService=navigationService;
        _appState=appState;
        up();
       
    }


    void up()
    {
        FrozeBooksIS=new ObservableCollection<Book>(Core.db.Books.Where(a=>a.Author==_appState.CurrentUser.UserId&&a.IsFrozen==true).ToList());
        _userFio=_appState.CurrentUser.fio;
        Mail = _appState.CurrentUser.Email;
        Phone = _appState.CurrentUser.PhoneNumber;
        BooksIS=new ObservableCollection<Book>(Core.db.Books.Where(a=>a.Author==_appState.CurrentUser.UserId).ToList());

    }

    [RelayCommand]
    public void CLickFreezeBook(Book book)
    {
        _appState.CurrentBook = book;
        _appState.ReportType = BidTypes.FreezeBookReport;
        _navigationService.ReplaceToAsync<BidPageViewModel>();
    }
    
    [RelayCommand]
    public void CLickBook(Book book)
    {
        _appState.CurrentBook = book;
        
    }

    [RelayCommand]
    public void AddBookClick()
    {
        
    }
    
}