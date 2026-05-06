using Avalonia.Controls;

namespace UP.Services;

public class NavigationService
{
    private NavigationPage _page;

    private async void attached(NavigationPage page)
    {
        _page = page;
    }

    private async void PushToAsync(ContentPage page)
    {
        await _page.PushAsync(page);
    }

    private async void PopToAsync(ContentPage page)
    {
        await _page.PopToRootAsync();
    }
    private async void PushModalAsync(ContentPage page)
    {
        await _page.PushModalAsync(page);
    }

    private async void PopModalAsync(ContentPage page)
    {
        await _page.PopModalAsync();
    }
}