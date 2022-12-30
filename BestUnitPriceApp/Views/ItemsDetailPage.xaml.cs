namespace BestUnitPriceApp.Views;

public partial class ItemsDetailPage : ContentPage
{
    public ItemsDetailPage(ItemsDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}
