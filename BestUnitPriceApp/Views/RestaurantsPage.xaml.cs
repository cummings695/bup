namespace BestUnitPriceApp.Views;

public partial class RestaurantsPage : ContentPage
{
    RestaurantsViewModel ViewModel;

    public RestaurantsPage(RestaurantsViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = ViewModel = viewModel;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await ViewModel.LoadDataAsync();
    }
}
