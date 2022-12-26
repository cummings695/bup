using static BestUnitPriceApp.Services.UnitService;

namespace BestUnitPriceApp.Services;


public class DialogService //: IDialogService
{
    //private static Page CurrentMainPage { get { return Application.Current.MainPage; } }

    //public async void ShowAlert(string title, string message, Action onClosed = null)
    //{
    //    await CurrentMainPage.DisplayAlert(title, message, TextResources.ButtonOK);
    //    onClosed?.Invoke();
    //}

    //public async Task<string> ShowActionSheet(string title, string cancel, string destruction = null, string[] buttons = null)
    //{
    //    var displayButtons = buttons ?? new string[] { };
    //    var action = await CurrentMainPage.DisplayActionSheet(title, cancel, destruction, displayButtons);
    //    return action;
    //}
}



public interface IDialogService
{
    Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);

    Task<bool> DisplayConfirm(string title, string message, string accept, string cancel);
}