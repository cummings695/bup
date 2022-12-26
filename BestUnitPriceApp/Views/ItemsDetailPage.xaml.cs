namespace BestUnitPriceApp.Views;

public partial class ItemsDetailPage : ContentPage
{
    public ItemsDetailPage(ItemsDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
