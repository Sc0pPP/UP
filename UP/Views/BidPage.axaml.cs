using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views;

public partial class BidPage : ContentPage
{
    public BidPage(BidPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    
    public BidPage()
    {
        InitializeComponent();
    }
}