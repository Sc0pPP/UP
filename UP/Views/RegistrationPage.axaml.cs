using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage(RegistrationPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}