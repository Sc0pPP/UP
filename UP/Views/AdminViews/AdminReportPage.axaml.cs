using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views.AdminViews;

public partial class AdminReportPage : ContentPage
{
    public AdminReportPage(AdminReportPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext=viewModel;
    }
    
    public AdminReportPage()
    {
        InitializeComponent();
    }
}