using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views.AdminViews;

public partial class AdminAuthorBidPage : ContentPage
{
    public AdminAuthorBidPage(AdminAuthorBidPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
    
    public AdminAuthorBidPage()
    {
        InitializeComponent();
    }
}