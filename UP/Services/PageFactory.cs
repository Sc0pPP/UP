using System;
using Avalonia.Controls;
using Azure;
using Microsoft.Extensions.DependencyInjection;
using UP.ViewModels;
using UP.Views;
using UP.Views.AdminViews;

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
            var t when t == typeof(CurrentBookPageViewModel)=>_serviceProvider.GetRequiredService<CurrentBookPage>(),
            var t when t == typeof(FirstPageViewModel)=>_serviceProvider.GetRequiredService<FirstPage>(),
            var t when t == typeof(ProfilePageViewModel)=>_serviceProvider.GetRequiredService<ProfilePage>(),
            var t when t == typeof(ListsPageViewModel)=>_serviceProvider.GetRequiredService<ListsPage>(),
            var t when t == typeof(AdminPageViewModel)=>_serviceProvider.GetRequiredService<AdminPage>(),
            var t when t == typeof(FreezePageViewModel)=> _serviceProvider.GetRequiredService<FreezePage>(),
            var t when t == typeof(AuthorPageViewModel)=>_serviceProvider.GetRequiredService<AuthorPage>(),
            var t when t == typeof(BidPageViewModel)=>_serviceProvider.GetRequiredService<BidPage>(),
            var t when t == typeof(EditBookAuthorPageViewModel)=>_serviceProvider.GetRequiredService<EditBookAuthorPage>(),
            var t when t == typeof(AddBookPageViewModel)=>_serviceProvider.GetRequiredService<AddBookPage>(),
            var t when t == typeof(AdminAuthorBidPageViewModel)=>_serviceProvider.GetRequiredService<AdminAuthorBidPage>(),
            var t when t == typeof(AdminFrozenBidPageViewModel)=>_serviceProvider.GetRequiredService<AdminFrozenBidPage>(),
            var t when t == typeof(AdminReportPageViewModel)=>_serviceProvider.GetRequiredService<AdminReportPage>(),
            var t when t == typeof(AdminFrozenListPageViewModel)=>_serviceProvider.GetRequiredService<AdminFrozenListPage>(),
            var t when t == typeof(AdminUsersListPageViewModel)=>_serviceProvider.GetRequiredService<AdminUsersListPage>()
            
        };
    }
}