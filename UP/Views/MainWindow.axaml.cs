using Avalonia.Controls;
using UP.Services;
using UP.ViewModels;

namespace UP.Views;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel,NavigationService navigationService,AuthPage authPage)
    {
        InitializeComponent();
        DataContext = viewModel;
        NavPage.Content=authPage;
        navigationService.Attached(NavPage);
    }
    
}