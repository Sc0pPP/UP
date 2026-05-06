using System;
using Avalonia.Controls;
using Azure;
using Microsoft.Extensions.DependencyInjection;
using UP.ViewModels;
using UP.Views;

namespace UP.Services;

public class PageFactory
{
    private readonly IServiceProvider _serviceProvider;

    public PageFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Page CreatePage<TViewModel>()
    {
        return typeof(TViewModel) switch
        {
            var t when t == typeof(AuthPageViewModel) => _serviceProvider.GetRequiredService<AuthPage>(),
            var t when t == typeof(RegistrationPageViewModel) => _serviceProvider.GetRequiredService<RegistrationPage>(),
        };
    }
}