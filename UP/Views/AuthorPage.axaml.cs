using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views;

public partial class AuthorPage : ContentPage
{
    public AuthorPage(AuthorPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    public AuthorPage()
    {
        InitializeComponent();
    }
}