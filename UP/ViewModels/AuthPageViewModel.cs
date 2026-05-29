using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UP.Context;
using UP.Models;
using UP.Services;
using UP.Views;

namespace UP.ViewModels;

public partial class AuthPageViewModel:ObservableObject
{
    private readonly NavigationService _navigationService;
    private readonly AppState _appState;
    [ObservableProperty]
    private bool _authButtonIsEnabled=false;
    [ObservableProperty]
    private bool _registrButtonIsEnabled=true;

    [ObservableProperty] 
    private bool _visibleRegistr = false;
    
    [ObservableProperty]
    private string _buttonText="Войти";

    [ObservableProperty] 
    private string _userNameTextBox;
    
    [ObservableProperty] 
    private string _passwordTextBox;
    
    [ObservableProperty] 
    private string _repeatPasswordTextBox;
    
    public AuthPageViewModel(NavigationService navigationService,AppState appState)
    {
        _appState = appState;
        _navigationService = navigationService;
        
    }
    
    [RelayCommand]
    public void RegistrClick()
    {
        _navigationService.ReplaceToAsync<RegistrationPageViewModel>();
    }
    
    [RelayCommand]
    public void AuthClick()
    {
        AuthButtonIsEnabled = false;
        RegistrButtonIsEnabled = true;
        VisibleRegistr = false;
        ButtonText = "Войти";
    }

    [RelayCommand]
    public async void MainButtonClick()
    {
        
        if (AuthButtonIsEnabled == false)
        {//Аутентификация
            if (string.IsNullOrWhiteSpace(UserNameTextBox) || string.IsNullOrWhiteSpace(PasswordTextBox) )
            {
                MessageBoxService.MessageBoxShow("Не все поля заполнены");
                return;
            }

            var data = new AuthData( UserNameTextBox, PasswordTextBox);
           
            
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000")
            };

            var response = await httpClient.PostAsJsonAsync("/api/auth/login",data);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBoxService.MessageBoxShow(error);
                return;
            }
             
            var users = await httpClient.GetFromJsonAsync<List<User>>("/api/users");

            foreach (User u in users )
            {
                if (u.Login == UserNameTextBox)
                {
                    _appState.CurrentUser = u;
                }
            }
            
            MessageBoxService.MessageBoxShow("Вы Вошли!");
            _navigationService.ReplaceToAsync<FirstPageViewModel>();
            
            // var user = Core.db.Users.FirstOrDefault(u => u.Login == UserNameTextBox && u.Password == PasswordTextBox);
            _navigationService.ReplaceToAsync<FirstPageViewModel>();
        }
    }
    
}