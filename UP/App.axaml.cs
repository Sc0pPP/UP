using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UP.Services;
using UP.ViewModels;
using UP.Views;

namespace UP;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
    private IHost _host;

    public override void OnFrameworkInitializationCompleted()
    {
        
        var builder = Host.CreateApplicationBuilder();
        //Service
        builder.Services.AddSingleton<NavigationService>();
        builder.Services.AddSingleton<PageFactory>();
        //Views
        builder.Services.AddSingleton<MainWindowViewModel>(); 
        builder.Services.AddSingleton< MainWindow>();
        builder.Services.AddTransient<AuthPage>();
        builder.Services.AddTransient<CurrentBookPage>();
        builder.Services.AddTransient<FirstPage>();
        builder.Services.AddTransient<CurrentBookPage>();
        builder.Services.AddTransient<RegistrationPage>();
        //ViewModels
        builder.Services.AddTransient<AuthPageViewModel>();
        builder.Services.AddTransient<FirstPageViewModel>();
        builder.Services.AddTransient<RegistrationPageViewModel>();
        
        _host = builder.Build();
        
        _host.Start();
        
        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}