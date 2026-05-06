using System;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public void MainButtonClick()
    {
        if (RegistrButtonIsEnabled == false)
        {//Регистрация
            /*
             if (string.IsNullOrWhiteSpace(UserNameTextBox) || string.IsNullOrWhiteSpace(PasswordTextBox) ||
                 string.IsNullOrWhiteSpace(RepeatPasswordTextBox))
            {
                WpfLikeAvaloniaMessageBox.MessageBox.Show("Не все поля заполнены");
                return;
            }
            if (Core.db.Users.FirstOrDefault(u => u.Login==UserNameTextBox) != null)
            {
                WpfLikeAvaloniaMessageBox.MessageBox.Show("Логин Занят");
                return;
            }

            if (PasswordTextBox != RepeatPasswordTextBox)
            {
                WpfLikeAvaloniaMessageBox.MessageBox.Show("Пароли не совпадают");
                return;
            }

            User newuser = new User()
            {

            };
            */

        }

        if (AuthButtonIsEnabled == false)
        {//Аутентификация
            if (string.IsNullOrWhiteSpace(UserNameTextBox) || string.IsNullOrWhiteSpace(PasswordTextBox) )
            {
                MessageBoxService.MessageBoxShow("Не все поля заполнены");
                return;
            }

            var user = Core.db.Users.FirstOrDefault(u => u.Login == UserNameTextBox && u.Password == PasswordTextBox);
            if (user==null)MessageBoxService.MessageBoxShow("Неверный логин или пароль");

            _appState.CurrentUser = user;
            _navigationService.PushToAsync<CurrentBookPageViewModel>();
        }
    }
    
}