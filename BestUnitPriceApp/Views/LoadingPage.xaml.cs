namespace BestUnitPriceApp.Views;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
