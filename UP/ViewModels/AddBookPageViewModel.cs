using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UP.Models;
using UP.Models.Requests;
using UP.Services;

namespace UP.ViewModels;

public partial class AddBookPageViewModel:ObservableObject
{
    private readonly NavigationService _navigationService;
    private readonly AppState _appState;
    public List<Genre> AddGenres =new List<Genre>();

    [ObservableProperty]
    private string _name;
    
    [ObservableProperty]
    private string _description;

    [ObservableProperty] 
    private string _review;

    [ObservableProperty] private string _context;

    [ObservableProperty] 
    private List<Genre> _allGenres;
    
    public AddBookPageViewModel(NavigationService navigationService,AppState appState)
    {
        _navigationService = navigationService;
        _appState = appState;
        AllGenres=new List<Genre>(Core.db.Genres.ToList());
    }

    [RelayCommand]
    public void AddGenre(Genre genre)
    {
        AddGenres.Add(genre);
        
    }

    [RelayCommand]
    public async void AddClick()
    {
        AddBook bk=new AddBook()
        {
            Name = Name,
            Description = Description,
            Author = _appState.CurrentUser.UserId,
            IsFrozen = true,
            CoverPath = "/covers/old_mansion.jpg",
            Content = Context,
            Review = 0,
            GenreIds = AddGenres.Select(g => g.GenreId).ToList()
            
        };
        using var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000")
        };
        
        
        var response = await httpClient.PostAsJsonAsync("/api/books/add", bk);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            MessageBoxService.MessageBoxShow(error);
            return;
        }
        
        MessageBoxService.MessageBoxShow("Книга наверн добавлена");
    }
    
}
