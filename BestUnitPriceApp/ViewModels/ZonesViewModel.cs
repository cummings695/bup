namespace BestUnitPriceApp.ViewModels;

public partial class ZonesViewModel : BaseViewModel
{
    private int _page = 1;
    private int _pageSize = 30;

    readonly IConnectivity _connectivity;
    private readonly IZoneService _zoneService;

    [ObservableProperty] bool isRefreshing;

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
        _page = 1;
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

        _page = 1;

        var results = await _zoneService.GetAsync();

        var zones = results.Match(
            items => items,
            exception => new List<Zone>());

        Zones = new ObservableCollection<Zone>(zones);

        IsBusy = false;
    }

    [RelayCommand]
    private async void GoToDetails(Vendor vendor)
    {
        //await Shell.Current.GoToAsync(nameof(ZonesDetailPage), true,
        //    new Dictionary<string, object> { { "Zone", Zone } });
    }
}