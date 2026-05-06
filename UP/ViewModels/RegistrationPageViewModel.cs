using System;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using UP.Models;
using UP.Services;

namespace UP.ViewModels;

public partial class RegistrationPageViewModel:ObservableObject
{
    private readonly NavigationService _navigationService;
    private readonly MessageBoxService _messageBoxService;
    private readonly AppState _appState;
    [ObservableProperty] 
    private string _userNameTextBox;
    
    [ObservableProperty] 
    private string _passwordTextBox;
    
    [ObservableProperty] 
    private string _repeatPasswordTextBox;
    
    [ObservableProperty] 
    private string _userFirstNameTextBox;
    
    [ObservableProperty] 
    private string _userLastNameTextBox;
    
    [ObservableProperty] 
    private string _userMidleNameTextBox;
    
    [ObservableProperty] 
    private string _mailTextBox;
    
    [ObservableProperty] 
    private string _phoneTextBox;
    

    public RegistrationPageViewModel(NavigationService navigationService,MessageBoxService messageBoxService,AppState appState)
    {
        _appState=appState;
        _messageBoxService = messageBoxService;
        _navigationService = navigationService;
    }

    [RelayCommand]
    public void AuthClick()
    {
        Console.Write("cnbdb");
    }

    [RelayCommand]
    public void MainButtonClick()
    {
        if (string.IsNullOrWhiteSpace(UserNameTextBox) || string.IsNullOrWhiteSpace(PasswordTextBox) ||
            string.IsNullOrWhiteSpace(RepeatPasswordTextBox))
        {
            MessageBoxService.MessageBoxShow("Не все поля регистрации заполнены");
            return;
        }
        if (string.IsNullOrWhiteSpace(UserLastNameTextBox) || string.IsNullOrWhiteSpace(UserMidleNameTextBox) ||
            string.IsNullOrWhiteSpace(UserFirstNameTextBox)|| string.IsNullOrWhiteSpace(MailTextBox)||
            string.IsNullOrWhiteSpace(PhoneTextBox))
        {
            MessageBoxService.MessageBoxShow("Не все поля Личных Данных заполнены");
            return;
        }
        if (Core.db.Users.FirstOrDefault(u => u.Login==UserNameTextBox) != null)
        {
            MessageBoxService.MessageBoxShow("Логин Занят");
            return;
        }

        if (PasswordTextBox != RepeatPasswordTextBox)
        {
            MessageBoxService.MessageBoxShow("Пароли не совпадают");
            return;
        }

        User RegUser = new User()
        {
            Login = UserNameTextBox,
            Password = PasswordTextBox,
            FirstName = UserFirstNameTextBox,
            MidleName = UserMidleNameTextBox,
            LastName = UserLastNameTextBox,
            Email = MailTextBox,
            PhoneNumber = PhoneTextBox,
            Role = 1,
            IsFrozen = false
        };
        Core.db.Users.Add(RegUser);
        Core.db.SaveChanges();
        _appState.CurrentUser=RegUser;
        MessageBoxService.MessageBoxShow("Вы зарегестрированны!");
        _navigationService.ReplaceToAsync<FirstPageViewModel>();
    }
}