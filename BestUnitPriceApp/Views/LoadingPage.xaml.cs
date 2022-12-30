namespace BestUnitPriceApp.Views;

public partial class LoadingPage : ContentPage
{
	private readonly LoadingViewModel ViewModel;

	public LoadingPage(LoadingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel = viewModel;
	}

	protected override async void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
		
		await ViewModel.CheckUserAuthentication();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
	}
}
