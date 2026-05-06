using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using UP.Services;

namespace UP.ViewModels;

public partial class RegistrationPageViewModel:ObservableObject
{
    private readonly NavigationService _navigationService;

    public RegistrationPageViewModel(NavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    
    
}