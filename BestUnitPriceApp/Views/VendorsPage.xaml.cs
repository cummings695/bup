namespace BestUnitPriceApp.Views;

public partial class VendorsPage : ContentPage
{
    VendorsViewModel ViewModel;

    public VendorsPage(VendorsViewModel viewModel)
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
