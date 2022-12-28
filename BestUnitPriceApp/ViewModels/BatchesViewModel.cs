namespace BestUnitPriceApp.ViewModels;

public partial class BatchesViewModel : BaseViewModel, IObserver<Restaurant>, IDisposable
{
    private int _page = 1;
    private int _pageSize = 25;
    
    readonly IConnectivity _connectivity;
    readonly IBatchService _batchService;

    [ObservableProperty] bool isRefreshing;

    [ObservableProperty] ObservableCollection<Batch> batches;
    
    private IDisposable _unsubscriber;

    public BatchesViewModel(
        ICurrentUserService currentUserService,
        IBatchService batchService,
        IConnectivity connectivity,
        SelectedRestaurantTracker selectedRestaurantTracker)
    {
        _batchService = batchService;
        _connectivity = connectivity;

        Title = "Order History";

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
        var result = await _batchService.GetAsync(_page, _pageSize);

        foreach (var item in result.Items)
        {
            Batches.Add(item);
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

        var result = await _batchService.GetAsync(_page, _pageSize);
        Batches = new ObservableCollection<Batch>(result.Items);
    }
    
    [RelayCommand]
    private async void GoToDetails(Batch batch)
    {
        await Shell.Current.GoToAsync(nameof(BatchesDetailPage), true, new Dictionary<string, object>
        {
            { "Batch", batch }
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
        LoadDataAsync().Wait();
    }
}