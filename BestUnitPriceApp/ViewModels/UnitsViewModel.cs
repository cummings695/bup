namespace BestUnitPriceApp.ViewModels;

public partial class UnitsViewModel : BaseViewModel
{
    private int _page = 1;
    private int _pageSize = 30;
    
    readonly IConnectivity _connectivity;
    private readonly IUnitService _unitService;

    [ObservableProperty] bool _isRefreshing;

    [ObservableProperty] ObservableCollection<Unit> _units;

    public UnitsViewModel(
        SampleDataService service,
        IUnitService unitService,
        IConnectivity connectivity
    )
    {
        _unitService = unitService;
        _connectivity = connectivity;
    }

    [RelayCommand]
    private async void OnRefreshing()
    {
        IsRefreshing = true;

        await LoadDataAsync();

        IsRefreshing = false;
    }

    [RelayCommand]
    public async Task LoadMore()
    {
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("No connectivity!",
                $"Please check internet and try again.", "OK");
            return;
        }

        if (IsBusy)
            return;

        IsRefreshing = IsBusy = true;

        _page += 1;
        var results = await _unitService.GetAsync(_page, _pageSize);

        var units = results.Match(
            result => result.Items,
            exception => new List<Unit>());

        foreach (var item in units)
        {
            Units.Add(item);
        }

        ;
        IsRefreshing = IsBusy = false;
    }

    public async Task LoadDataAsync()
    {
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("No connectivity!", $"Please check internet and try again.", "OK");
            return;
        }

        if (IsBusy)
            return;

        _page = 1;

        var results = await _unitService.GetAsync(_page, _pageSize);

        var units = results.Match(
            result => result.Items,
            exception => new List<Unit>());

        Units = new ObservableCollection<Unit>(units);

        IsBusy = false;
    }

    [RelayCommand]
    private async void GoToDetails(Unit unit)
    {
        //await Shell.Current.GoToAsync(nameof(UnitsDetailPage), true,
        //    new Dictionary<string, object> { { "Unit", unit } });
    }
}