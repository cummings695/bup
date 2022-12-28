namespace BestUnitPriceApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(VendorsDetailPage), typeof(VendorsDetailPage));
        Routing.RegisterRoute(nameof(RestaurantsDetailPage), typeof(RestaurantsDetailPage));
        Routing.RegisterRoute(nameof(ItemsDetailPage), typeof(ItemsDetailPage));
        Routing.RegisterRoute(nameof(BatchesDetailPage), typeof(BatchesDetailPage));
        Routing.RegisterRoute(nameof(OrdersDetailPage), typeof(OrdersDetailPage));
    }
}
