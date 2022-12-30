namespace BestUnitPriceApp.Views;

public partial class UnitsPage : ContentPage
{
	private readonly UnitsViewModel ViewModel;
	public UnitsPage(UnitsViewModel viewModel)
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