using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views;

public partial class AuthPage : ContentPage
{
    public AuthPage(AuthPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext=viewModel;
    }
    public AuthPage()
    {
        InitializeComponent();
    }
}