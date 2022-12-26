namespace BestUnitPriceApp.Views;

public partial class VendorsDetailPage : ContentPage
{
    public VendorsDetailPage(VendorsDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
