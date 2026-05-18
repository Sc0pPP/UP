using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views.AdminViews;

public partial class AdminUsersListPage : ContentPage
{
    public AdminUsersListPage(AdminUsersListPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
    
    public AdminUsersListPage()
    {
        InitializeComponent();
    }
}