namespace BestUnitPriceApp.ViewModels;

public partial class ZonesViewModel : BaseViewModel
{
    readonly IConnectivity _connectivity;
    private readonly IZoneService _zoneService;

    [ObservableProperty] bool _isRefreshing;

    [ObservableProperty] ObservableCollection<Zone> _zones;

    public ZonesViewModel(
        IZoneService zoneService,
        IConnectivity connectivity
    )
    {
        _zoneService = zoneService;
        _connectivity = connectivity;
    }

    [RelayCommand]
    private async void OnRefreshing()
    {
        IsRefreshing = true;
        await LoadDataAsync();
        IsRefreshing = false;
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

        IsBusy = true;

        var results = await _zoneService.GetAsync();

        var zones = results.Match(
            items => items,
            exception => new List<Zone>());

        Zones = new ObservableCollection<Zone>(zones);

        IsBusy = false;
    }

    [RelayCommand]
    private async void GoToDetails(Zone zone)
    {
        //await Shell.Current.GoToAsync(nameof(ZonesDetailPage), true,
        //    new Dictionary<string, object> { { "Zone", Zone } });
    }
}