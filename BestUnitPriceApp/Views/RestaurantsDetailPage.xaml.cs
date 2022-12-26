namespace BestUnitPriceApp.Views;

public partial class RestaurantsDetailPage : ContentPage
{
    public RestaurantsDetailPage(RestaurantsDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
