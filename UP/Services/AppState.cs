using CommunityToolkit.Mvvm.ComponentModel;
using UP.Models;

namespace UP.Services;

public partial class AppState:ObservableObject
{
    
    [ObservableProperty]
    private Book _currentBook;
    [ObservableProperty]
    private User? _currentUser;
    [ObservableProperty]
    private Review? _currentReview;

    [ObservableProperty] 
    private BidTypes _reportType;
    
   
}