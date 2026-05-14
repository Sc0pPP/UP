using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UP.Models;
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
    public void AddClick()
    {
        Book bk=new Book()
        {
            Name = Name,
            Description = Description,
            Author = _appState.CurrentUser.UserId,
            IsFrozen = true,
            CoverPath = "/covers/old_mansion.jpg",
            Content = Context,
            Genre = 0,
            Review = 0
        };
        
        Core.db.Books.Add(bk);
        Core.db.SaveChanges();
        int CurrentBookId = Core.db.Books.FirstOrDefault(b => b.Name == Name).BookId;
        foreach (Genre g in  AddGenres )
        {
            BookGenre bg = new BookGenre()
            {
                GenreId = g.GenreId,
                BookId = CurrentBookId
            };
            Core.db.BookGenres.Add(bg);
            Core.db.SaveChanges();
            
        }
        
        MessageBoxService.MessageBoxShow("Книга наверн добавлена");
    }
    
}