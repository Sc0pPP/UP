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

public partial class ListsPageViewModel : ObservableObject
{
    private readonly NavigationService _navigationService;
    private readonly AppState _appState;

    [ObservableProperty] 
    private ObservableCollection<ReadingList> _booksIS = new();
    [ObservableProperty] 
    private int _selectedListType = 2;
    [ObservableProperty] 
    private string _searchName = "";
    [ObservableProperty] 
    private string _searchAuthor = "";
    [ObservableProperty] 
    private List<string> _genresList = Core.db.Genres.Select(g => g.Genre1).ToList();
    [ObservableProperty] 
    private string _selectedGenre;

    public ListsPageViewModel(NavigationService navigationService, AppState appState)
    {
        _navigationService = navigationService;
        _appState = appState;
        filtered();
    }
    private void filtered()
    {
        var filteredAll = Core.db.ReadingLists
            .Include(r => r.Book)
            .ThenInclude(b => b.BookGenres)
            .ThenInclude(bg => bg.Genre)
            .Include(r => r.Book.AuthorNavigation)
            .Where(r => r.UserId == _appState.CurrentUser.UserId && r.ReadingListType == _selectedListType)
            .ToList();
        if (!string.IsNullOrWhiteSpace(_searchName))
        {
            filteredAll = filteredAll.Where(b =>
                b.Book.Name.Contains(_searchName, StringComparison.OrdinalIgnoreCase) 
            ).ToList();
        }

        if (!string.IsNullOrWhiteSpace(_searchAuthor))
        {
            filteredAll = filteredAll.Where(b =>
                b.Book.AuthorNavigation.fio.Contains(_searchAuthor, StringComparison.OrdinalIgnoreCase) 
            ).ToList();
        }

        if (!string.IsNullOrWhiteSpace(_selectedGenre))
        {
            filteredAll= filteredAll.Where(b => b.Book.BookGenres.Count(bg => bg.Genre.Genre1 == _selectedGenre) > 0).ToList();
        }
        BooksIS=new ObservableCollection<ReadingList>(filteredAll);
        
    }

    partial void OnSearchNameChanged (string value)
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
    private void PlansClick()
    {
        _selectedListType = 2;
        filtered();
    }

    [RelayCommand]
    private void ReadingClick()
    {
        _selectedListType  = 3;
        filtered();
    }
    [RelayCommand] private void ReadClick() { 
        _selectedListType  = 4;
        filtered();
    }

    [RelayCommand]
    private void СbandonedClick()
    {
        _selectedListType  = 1;
        filtered();
    }

    [RelayCommand]
    private void ClickBook(ReadingList item)
    {
        _appState.CurrentBook = item.Book;
        _navigationService.ReplaceToAsync<CurrentBookPageViewModel>();
    }

    [RelayCommand]
    private void MoveToList(ReadingList item)
    {
        item.ReadingListType = item.ReadingListType switch
        {
            1 => 2,
            2 => 3,
            3 => 4,
            4 => 1,
            _ => 2
        };
        Core.db.SaveChanges();
       filtered();
    }

    [RelayCommand]
    private void RemoveFromList(ReadingList item)
    {
        Core.db.ReadingLists.Remove(item);
        Core.db.SaveChanges();
        filtered();
    }
}