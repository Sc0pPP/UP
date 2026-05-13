using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UP.ViewModels;

namespace UP.Views;

public partial class EditBookAuthorPage : ContentPage
{
    public EditBookAuthorPage(EditBookAuthorPageViewModel vm)
    {
        InitializeComponent();
        DataContext = vm;
    }
    public EditBookAuthorPage()
    {
        InitializeComponent();
    }
}