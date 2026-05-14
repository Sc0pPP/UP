using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using UP.Models;
using UP.Services;

namespace UP.ViewModels;

public partial class EditBookAuthorPageViewModel:ObservableObject
{
    private readonly AppState _appState;
    private readonly NavigationService _navigationService;

    [ObservableProperty] 
    private string _name;
    
    [ObservableProperty]
    private string _author;
    
    [ObservableProperty]
    private ICollection<BookGenre> _bookGenres;

    [ObservableProperty] 
     private Book _currentBook;
    
    [ObservableProperty] 
    private string _longText;

    [ObservableProperty] 
    private string _description;

    [ObservableProperty]
    private List<Genre> _allGenres;
    
    [ObservableProperty]
    private Genre _currentGenre;

 

    public EditBookAuthorPageViewModel(NavigationService navigationService,AppState appState)
    {
        _appState = appState;
        _navigationService = navigationService;
        AllGenres= new List<Genre>(Core.db.Genres.ToList());
        CurrentBook=Core.db.Books.Include(b=>b.BookGenres).FirstOrDefault(b => b.BookId==_appState.CurrentBook.BookId);
        LongText = CurrentBook.Content;
        Name = CurrentBook.Name;
        Description = CurrentBook.Description;
        Author = CurrentBook.AuthorNavigation.fio;
        
    }

    [RelayCommand]
    public void SaveClick()
    {
        Core.db.SaveChanges();
    }

    [RelayCommand]
    public void AddGenre(Genre genre)
    {
        BookGenre bookGenre = new BookGenre()
        {
            BookId = _appState.CurrentBook.BookId,
            GenreId = genre.GenreId
        };
        Core.db.BookGenres.Add(bookGenre);
        Core.db.SaveChanges();
    }
    
}