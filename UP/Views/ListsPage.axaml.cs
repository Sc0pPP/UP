using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views;

public partial class ListsPage : ContentPage
{
    public ListsPage(ListsPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    public ListsPage()
    {
        InitializeComponent();
    }
}