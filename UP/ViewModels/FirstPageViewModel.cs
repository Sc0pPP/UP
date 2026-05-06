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
   
    [ObservableProperty]
    private string _text = "";
    [ObservableProperty]
    private ObservableCollection<Book> _booksIS = new();
    [ObservableProperty]
    private Book _selectedBook;

    [ObservableProperty]
    private string _searchName;

    [ObservableProperty] 
    private string _searchAuthor;
    
    
    public FirstPageViewModel()
    {
        
        List<Book> Books=Core.db.Books.ToList();
        BooksIS = new ObservableCollection<Book>(Core.db.Books
            .Include(b=>b.BookGenres)
            .ThenInclude(b=>b.Genre)
            .Include(b=>b.AuthorNavigation)
            .ToList());
        
    }
    

    [RelayCommand]
    private void CLickBook(Book book)
    {
        AppState.Instance.CurrentBook=book;
    }
}

