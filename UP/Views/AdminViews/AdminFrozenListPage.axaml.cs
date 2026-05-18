using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views.AdminViews;

public partial class AdminFrozenListPage : ContentPage
{
    public AdminFrozenListPage(AdminFrozenListPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
    
    public AdminFrozenListPage()
    {
        InitializeComponent();
    }
}