using System.Reflection.PortableExecutable;

namespace BestUnitPriceApp.ViewModels;

public partial class ItemsViewModel : BaseViewModel, IObserver<Restaurant>, IDisposable
{
    private int _page = 1;
    private int _pageSize = 50;

    readonly SampleDataService dataService;
    readonly IConnectivity _connectivity;
    readonly IInventoryItemService _inventoryItemService;
    readonly IZoneService _zoneService;

    [ObservableProperty] bool isRefreshing;

    [ObservableProperty] private Zone _selectedZone;

    [ObservableProperty] ObservableCollection<InventoryItem> items;
    [ObservableProperty] ObservableCollection<Zone> zones;
    
    private IDisposable _unsubscriber;

    public ItemsViewModel(SampleDataService service,
        ICurrentUserService currentUserService,
        IZoneService zoneService,
        IInventoryItemService inventoryItemService,
        IConnectivity connectivity,
        SelectedRestaurantTracker selectedRestaurantTracker)
    {
        dataService = service;
        _zoneService = zoneService;
        _inventoryItemService = inventoryItemService;
        _connectivity = connectivity;

        Title = "Items";

        if (selectedRestaurantTracker != null)
            _unsubscriber = selectedRestaurantTracker.Subscribe(this);
    }

    [RelayCommand]
    private async void OnRefreshing()
    {
        IsRefreshing = true;
        _page = 1;
        try
        {
            await LoadZonesAsync();
            await LoadDataAsync();
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    public async Task LoadMore()
    {
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("No connectivity!", $"Please check internet and try again.", "OK");
            return;
        }

        _page += 1;
        var result = await _inventoryItemService.GetByZoneAsync(this._selectedZone.Id, _page, _pageSize);

        foreach (var item in result.Items)
        {
            Items.Add(item);
        }
    }

    public async Task LoadDataAsync()
    {
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("No connectivity!", $"Please check internet and try again.", "OK");
            return;
        }

        var result = await _inventoryItemService.GetByZoneAsync(this._selectedZone.Id, _page, _pageSize);
        Items = new ObservableCollection<InventoryItem>(result.Items);
    }

    public async Task LoadZonesAsync()
    {
        if (IsBusy)
            return;

        try
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            IsBusy = true;
            var result = await _zoneService.GetAsync();
            Zones = result.Match(
                z => { return new ObservableCollection<Zone>(z.OrderBy(z => z.SortOrder)); },
                e => { return new ObservableCollection<Zone>(); });
            //Zones = new ObservableCollection<Zone>(result.Zones.OrderBy(z => z.SortOrder));
            Zones.Add(new Zone { Id = -1, Name = "NO ZONE" });
            if (Zones.Any())
                this.SelectedZone = Zones.First();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get Zones: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async void GoToDetails(SampleItem item)
    {
        await Shell.Current.GoToAsync(nameof(ItemsDetailPage), true, new Dictionary<string, object>
        {
            { "Item", item }
        });
    }

    public void Dispose()
    {
        _unsubscriber.Dispose();
    }

    public void OnCompleted()
    {
        Console.WriteLine("The Restaurant Tracker has completed transmitting data to {0}.", nameof(ItemsViewModel));
        _unsubscriber.Dispose();
    }

    public void OnError(Exception error)
    {
        //throw new NotImplementedException();
    }

    public void OnNext(Restaurant value)
    {
        //Title = value.Name;
        // reload the listing based upon the new restaurant.
        //LoadZonesAsync().Wait();
        LoadDataAsync().Wait();
        // throw new NotImplementedException();
    }
}