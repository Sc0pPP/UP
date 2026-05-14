using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views;

public partial class AddBookPage : ContentPage
{
    public AddBookPage(AddBookPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    public AddBookPage()
    {
        InitializeComponent();
    }
}