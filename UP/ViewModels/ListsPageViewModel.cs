using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using UP.Models;
using UP.Models.Requests;
using UP.Services;

namespace UP.ViewModels;

public partial class ListsPageViewModel : ObservableObject
{
    private readonly NavigationService _navigationService;
    private readonly AppState _appState;

    [ObservableProperty] 
    private ObservableCollection<ReadingListResponse> _booksIS = new();
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
    
    [ObservableProperty]
    private List<string> _authorCombo = new List<string>{"По умолчанию", "По возрастанию", "По Убыванию"};

    [ObservableProperty]
    private List<string> _ratingCombo=new List<string>{"По умолчанию", "По возрастанию", "По Убыванию"};

    [ObservableProperty] 
    private string _selectedAuthorCombo = "По умолчанию";
    
    [ObservableProperty] 
    private string _selectedRatingCombo = "По умолчанию";

    public ListsPageViewModel(NavigationService navigationService, AppState appState)
    {
        _navigationService = navigationService;
        _appState = appState;
        filtered();
    }
    private async void  filtered()
    {
        using var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000")
        };
        
       
        var readingList = await httpClient.GetFromJsonAsync<List<ReadingListResponse>>(
            $"/api/readinglists?userId={_appState.CurrentUser.UserId}&type={_selectedListType}");
        var filteredAll = readingList ?? new List<ReadingListResponse>();
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
            filteredAll = filteredAll.Where(b => b.Genres.Contains(_selectedGenre)).ToList();
        }
        
        if (SelectedAuthorCombo != "По умолчанию")
        {
            if (SelectedAuthorCombo == "По возрастанию")
            {
                filteredAll = filteredAll.OrderBy(b => b.Book.AuthorNavigation.fio).ToList();
            }

            if (SelectedAuthorCombo == "По Убыванию")
            {
                filteredAll = filteredAll.OrderByDescending(b => b.Book.AuthorNavigation.fio).ToList();
            }
        } 
        if (SelectedRatingCombo != "По умолчанию")
        {
            if (SelectedRatingCombo == "По возрастанию")
            {
                filteredAll = filteredAll.OrderBy(b => b.Book.Review).ToList();
            }

            if (SelectedRatingCombo == "По Убыванию")
            {
                filteredAll = filteredAll.OrderByDescending(b => b.Book.Review).ToList();
            }
        } 
        BooksIS=new ObservableCollection<ReadingListResponse>(filteredAll);
        
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
    
    partial void OnSelectedAuthorComboChanged(string value)
    {
        filtered();
    }
    
    partial void OnSelectedRatingComboChanged(string value)
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
    private void ClickBook(ReadingListResponse item)
    {
        _appState.CurrentBook = item.Book;
        _navigationService.ReplaceToAsync<CurrentBookPageViewModel>();
    }

    [RelayCommand]
    private async void MoveToList(ReadingListResponse item)
    {
        EditRL editrl = new EditRL()
        {
            ReadingListId = item.Rlid,
            ReadingListType = item.ReadingListType,
        };
        editrl.ReadingListType = editrl.ReadingListType switch
        {
            1 => 2,
            2 => 3,
            3 => 4,
            4 => 1,
            _ => 2
        };
        using var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000")
        };
        
        
        var response = await httpClient.PatchAsJsonAsync("/api/readinglists/edit", editrl);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            MessageBoxService.MessageBoxShow(error);
            return;
        }
       filtered();
    }
    

    [RelayCommand]
    private async void RemoveFromList(ReadingListResponse item)
    {
        using var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000")
        };

        var response = await httpClient.DeleteAsync($"/api/readinglists/{item.Rlid}");

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            MessageBoxService.MessageBoxShow(error);
            return;
        }

        filtered();
    }
}
