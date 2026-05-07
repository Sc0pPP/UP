using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views;

public partial class AdminPage : ContentPage
{
    public AdminPage(AdminPageViewModel adminPageViewModel)
    {
        InitializeComponent();
        DataContext = adminPageViewModel;
    }
    public AdminPage()
    {
        InitializeComponent();
    }
}