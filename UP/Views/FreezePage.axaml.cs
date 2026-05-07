using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views;

public partial class FreezePage : ContentPage
{
    public FreezePage(FreezePageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    public FreezePage()
    {
        InitializeComponent();
    }
}