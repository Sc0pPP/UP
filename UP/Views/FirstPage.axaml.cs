using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views;

public partial class FirstPage : ContentPage
{
    public FirstPage(FirstPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext=viewModel;
    }
    public FirstPage()
    {
        InitializeComponent();
       
    }
}