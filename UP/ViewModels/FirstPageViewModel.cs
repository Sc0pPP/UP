using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using UP.Models;
using UP.Services;

namespace UP.ViewModels;

public partial class FirstPageViewModel : ObservableObject
{
   private readonly NavigationService _navigationService;
    [ObservableProperty]
    private string _text = "";
    [ObservableProperty]
    private ObservableCollection<Book> _booksIS = new();
    [ObservableProperty]
    private Book _selectedBook;

    [ObservableProperty]
    private string _searchName="";

    [ObservableProperty] 
    private string _searchAuthor="";
    
    [ObservableProperty]
    List<Book> _books=Core.db.Books.ToList();

    [ObservableProperty]
    private List<string> _genresList=Core.db.Genres.Select(g=>g.Genre1).ToList();

    [ObservableProperty] 
    private string _selectedGenre;

    public FirstPageViewModel(NavigationService navigationService)
    {
        
        _navigationService = navigationService;
        Books=Core.db.Books.ToList();
        BooksIS = new ObservableCollection<Book>(Core.db.Books
            .Include(b=>b.BookGenres)
            .ThenInclude(b=>b.Genre)
            .Include(b=>b.AuthorNavigation)
            .ToList());
        
    }

    private void filtered()
    {
        var filteredAll = Books;
        if (!string.IsNullOrWhiteSpace(_searchName))
        {
            filteredAll = filteredAll.Where(b =>
                b.Name.Contains(SearchName, StringComparison.OrdinalIgnoreCase) 
            ).ToList();
        }

        if (!string.IsNullOrWhiteSpace(_searchAuthor))
        {
            filteredAll = filteredAll.Where(b =>
                b.AuthorNavigation.fio.Contains(SearchAuthor, StringComparison.OrdinalIgnoreCase) 
            ).ToList();
        }

        if (!string.IsNullOrWhiteSpace(_selectedGenre))
        {
            filteredAll= filteredAll.Where(b => b.BookGenres.Count(bg => bg.Genre.Genre1 == SelectedGenre) > 0).ToList();
        }
        BooksIS=new ObservableCollection<Book>(filteredAll);
        
    }
    
    
    partial void OnSearchNameChanged(string value)
    {
        filtered();
    }
    partial void OnSearchAuthorChanged(string value)
    {
        filtered();
    }
    partial void OnSelectedGenreChanged(string value)
    {
        filtered();
    }
    

    [RelayCommand]
    private void CLickBook(Book book)
    {
        AppState.Instance.CurrentBook=book;
    }
}

