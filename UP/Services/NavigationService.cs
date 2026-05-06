using Avalonia.Controls;

namespace UP.Services;

public class NavigationService
{
    private readonly PageFactory _pageFactory;

    public NavigationService(PageFactory pageFactory)
    {
        _pageFactory = pageFactory;
    }
    private NavigationPage _page;

    public async void Attached(NavigationPage page)
    {
        _page = page;
    }

    public  async void PushToAsync<TViewModel>()
    {
        await _page.PushAsync(_pageFactory.CreatePage<TViewModel>());
    }

    private async void PopToAsync()
    {
        await _page.PopAsync();
    }
    private async void PushModalAsync<TViewModel>()
    {
        await _page.PushModalAsync(_pageFactory.CreatePage<TViewModel>());
    }

    private async void PopModalAsync()
    {
        await _page.PopModalAsync();
    }
    public  async void ReplaceToAsync<TViewModel>()
    {
        await _page.ReplaceAsync(_pageFactory.CreatePage<TViewModel>());
    }
}