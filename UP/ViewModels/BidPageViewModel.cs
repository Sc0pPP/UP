using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UP.Models;
using UP.Services;

namespace UP.ViewModels;

public partial class BidPageViewModel : ObservableObject
{
    private readonly AppState _appState;

    private readonly NavigationService _navigationService;

    [ObservableProperty] private string _cause = string.Empty;

    [ObservableProperty] private string _targetDescription = string.Empty;
    [ObservableProperty] private IEnumerable<BidTypes> _reportTypes = Enum.GetValues<BidTypes>();


    public BidPageViewModel(AppState appState, NavigationService navigationService)
    {
        _appState = appState;
        _navigationService = navigationService;
        TargetDescription = appState.ReportType switch
        {
            BidTypes.BookReport or BidTypes.FreezeBookReport =>
                $"Книга: {appState.CurrentBook?.Name}",

            BidTypes.ReviewReport =>
                $"Отзыв: {appState.CurrentReview?.ReviewText?[..Math.Min(50, appState.CurrentReview.ReviewText.Length)]}...",

            BidTypes.AuthorReport or BidTypes.FreezeAuthorReport =>
                $"Автор книги: {appState.CurrentBook?.Name}",

            BidTypes.FreezeUserReport =>
                $"Заморозка отзыва",
            BidTypes.AuthorBid=>
                $"Заявка на получение роли автора",

            _ => string.Empty
        };
    }

    [RelayCommand]
    private async Task Submit()
    {
        if (string.IsNullOrWhiteSpace(Cause)) return;

        switch (_appState.ReportType)
        {
            case BidTypes.BookReport:
                await Core.db.Reports.AddAsync(new Report
                {
                    UserId = _appState.CurrentUser!.UserId,
                    BookId = _appState.CurrentBook!.BookId,
                    Cause = Cause,
                    IsChecked = false
                });
                break;

            case BidTypes.ReviewReport:
                await Core.db.Reports.AddAsync(new Report
                {
                    UserId = _appState.CurrentUser!.UserId,
                    ReviewId = _appState.CurrentReview!.ReviewId,
                    Cause = Cause,
                    IsChecked = false
                });
                break;

            case BidTypes.AuthorReport:
                await Core.db.Reports.AddAsync(new Report
                {
                    UserId = _appState.CurrentUser!.UserId,
                    ReportedUserId = _appState.CurrentBook!.Author,
                    Cause = Cause,
                    IsChecked = false
                });
                break;

            case BidTypes.FreezeBookReport:
                await Core.db.FrozenBids.AddAsync(new FrozenBid
                {
                    UserId = _appState.CurrentUser!.UserId,
                    BookId = _appState.CurrentBook!.BookId,
                    Bid = Cause
                });
                break;

                    case BidTypes.FreezeAuthorReport:
                        await Core.db.FrozenBids.AddAsync(new FrozenBid
                        {
                            UserId = _appState.CurrentUser!.UserId,
                            ReportedUserId = _appState.CurrentBook!.Author,
                            Bid = Cause
                        });
                        break;
            
                    case BidTypes.FreezeUserReport:
                        await Core.db.FrozenBids.AddAsync(new FrozenBid
                        {
                            UserId = _appState.CurrentUser!.UserId,
                            //ReportedUserId = _appState.CurrentReview!.UserId,
                            Bid = Cause
                        });
                        break;
                }
            
                await Core.db.SaveChangesAsync();
                _navigationService.ReplaceToAsync<ProfilePageViewModel>();
            }
            
            [RelayCommand]
            private void Cancel()
            {
                _navigationService.ReplaceToAsync<ProfilePageViewModel>();
            }
        }
    
    