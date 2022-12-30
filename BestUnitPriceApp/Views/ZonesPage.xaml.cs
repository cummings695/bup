namespace BestUnitPriceApp.Views;

public partial class ZonesPage : ContentPage
{
	private readonly ZonesViewModel ViewModel;
	public ZonesPage(ZonesViewModel viewModel)
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