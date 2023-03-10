using BestUnitPriceApp.Common.Messages;
using CommunityToolkit.Mvvm.Messaging;
using System.Reflection.PortableExecutable;

namespace BestUnitPriceApp.ViewModels;

public partial class ItemsViewModel : BaseViewModel
{
    private int _page = 1;
    private int _pageSize = 50;
    
    readonly IConnectivity _connectivity;
    readonly IInventoryItemService _inventoryItemService;
    readonly IZoneService _zoneService;

    [ObservableProperty] bool _isRefreshing;

    [ObservableProperty] private Zone _selectedZone;

    [ObservableProperty] ObservableCollection<InventoryItem> _items;
    [ObservableProperty] ObservableCollection<Zone> _zones;
    

    public ItemsViewModel(SampleDataService service,
        ICurrentUserService currentUserService,
        IZoneService zoneService,
        IInventoryItemService inventoryItemService,
        IConnectivity connectivity)
    {
        _zoneService = zoneService;
        _inventoryItemService = inventoryItemService;
        _connectivity = connectivity;

        Title = "Items";

        WeakReferenceMessenger.Default.Register<SelectedRestaurantChangedMessage>(this, (rec, m) =>
        {
            this.Title = $"{m.Value.Name} Items";
            OnRefreshing();
        });
    }

    [RelayCommand]
    private async void OnRefreshing()
    {
        IsRefreshing = true;
        _page = 1;
        try
        {
            //await LoadZonesAsync();
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
        var result = await _inventoryItemService.GetByZoneAsync(this._selectedZone.Id, _page, _pageSize, true);

        foreach (var item in result.Items)
        {
            Items.Add(item);
        }
    }

    public async Task LoadDataAsync()
    {
        _page = 1;
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("No connectivity!", $"Please check internet and try again.", "OK");
            return;
        }

        var result = await _inventoryItemService.GetByZoneAsync(this._selectedZone.Id, _page, _pageSize, true);
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
    private async void GoToDetails(InventoryItem item)
    {
        await Shell.Current.GoToAsync(nameof(ItemsDetailPage), true, new Dictionary<string, object>
        {
            { "InventoryItem", item }
        });
    }
}