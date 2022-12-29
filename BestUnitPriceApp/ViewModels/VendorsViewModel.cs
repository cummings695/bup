namespace BestUnitPriceApp.ViewModels;

public partial class VendorsViewModel : BaseViewModel
{
    private int _page = 1;
    private int _pageSize = 30;

    readonly SampleDataService dataService;
    readonly IConnectivity _connectivity;
    private readonly IVendorService _vendorService;

    [ObservableProperty] bool isRefreshing;

    [ObservableProperty] ObservableCollection<Vendor> _vendors;

    public VendorsViewModel(
        SampleDataService service,
        IVendorService vendorService,
        IConnectivity connectivity
    )
    {
        dataService = service;
        _vendorService = vendorService;
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
        
        _page += 1;
        var results = await _vendorService.GetAsync(_page, _pageSize);

        var vendors = results.Match(
            vendors => { return vendors.Items; },
            exception => { return new List<Vendor>(); });

        foreach (var item in vendors)
        {
            Vendors.Add(item);
        }

        IsBusy = false;
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

        var results = await _vendorService.GetAsync(_page, _pageSize);

        var vendors = results.Match(
            vendors => { return vendors.Items; },
            exception => { return new List<Vendor>(); });

        Vendors = new ObservableCollection<Vendor>(vendors);

        IsBusy = false;
    }

    [RelayCommand]
    private async void GoToDetails(Vendor vendor)
    {
        await Shell.Current.GoToAsync(nameof(VendorsDetailPage), true,
            new Dictionary<string, object> { { "Vendor", vendor } });
    }
}