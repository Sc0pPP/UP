using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views.AdminViews;

public partial class AdminFrozenBidPage : ContentPage
{
    public AdminFrozenBidPage(AdminFrozenBidPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        
    }
    
    public AdminFrozenBidPage()
    {
        InitializeComponent();
    }
}