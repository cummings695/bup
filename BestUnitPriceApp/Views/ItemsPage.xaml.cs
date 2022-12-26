namespace BestUnitPriceApp.Views;

public partial class ItemsPage : ContentPage
{
    ItemsViewModel ViewModel;

    public ItemsPage(ItemsViewModel viewModel)
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
