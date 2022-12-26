namespace BestUnitPriceApp.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    //protected readonly ICurrentUserService CurrentUserService;

    //[ObservableProperty] private ApplicationUser _user = null;

    //public BaseViewModel(ICurrentUserService currentUserService)
    //{
    //    CurrentUserService = currentUserService;
    //    _user = currentUserService.Get();
    //}

    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _title;

    public bool IsNotBusy => !IsBusy;
}
