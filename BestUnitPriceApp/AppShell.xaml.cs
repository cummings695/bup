namespace BestUnitPriceApp;

public partial class AppShell : Shell
{
	private readonly AppShellViewModel ViewModel;

	public AppShell()
	{
		InitializeComponent();

		BindingContext = ViewModel = new AppShellViewModel();
		RegisterRoutes();
	}

	private void RegisterRoutes()
	{
		Routing.RegisterRoute(nameof(VendorsDetailPage), typeof(VendorsDetailPage));
		Routing.RegisterRoute(nameof(RestaurantsDetailPage), typeof(RestaurantsDetailPage));
		Routing.RegisterRoute(nameof(ItemsDetailPage), typeof(ItemsDetailPage));
		Routing.RegisterRoute(nameof(BatchesDetailPage), typeof(BatchesDetailPage));
		Routing.RegisterRoute(nameof(OrdersDetailPage), typeof(OrdersDetailPage));


		Routing.RegisterRoute(nameof(ScanInvoicePage), typeof(ScanInvoicePage));
	}

	//protected override async void OnNavigated(ShellNavigatedEventArgs args)
	//{
	//    base.OnNavigated(args);


	//}


}
