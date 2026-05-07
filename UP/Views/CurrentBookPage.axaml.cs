using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views;

public partial class CurrentBookPage : ContentPage
{
    public CurrentBookPage(CurrentBookPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext=viewModel;
    }

    public CurrentBookPage()
    {
        InitializeComponent();  
    }
    
}