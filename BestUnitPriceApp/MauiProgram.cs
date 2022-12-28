using BestUnitPriceApp.Views;

namespace BestUnitPriceApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
                fonts.AddFont("Verdana.tff", "verdana");
                fonts.AddFont("Verdana-Bold.tff", "verdana-bold");
                fonts.AddFont("Verdana-Italic.tff", "verdana-italic");
                fonts.AddFont("Verdana-Bold-Italic.tff", "verdana-bold-italic");

                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                fonts.AddFont("BlackOpsOne-Regular.ttf", "BlackOpsOne");
                //fonts.AddFont("Font-Awesome-DuoTone.otf", "FAD");
                fonts.AddFont("Font-Awesome-Solid.otf", "FontAwesome");
                //fonts.AddFont("Font-Awesome-Light.otf", "FAL");
                //fonts.AddFont("Font-Awesome-Thin.otf", "FAT");
                //fonts.AddFont("Font-Awesome-Brands.otf", "FAB");

                fonts.AddFont("SF-Pro-Display-Regular.otf", "Frisco");
            });

		builder.Services.AddHttpClient<IRestaurantService, RestaurantService>();
		//.SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Set lifetime to five minutes
		//.AddPolicyHandler(GetRetryPolicy());
		/**/
		builder.Services.AddTransient<HttpClient>();
		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		builder.Services.AddSingleton<IMap>(Map.Default);

		builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
		builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
		builder.Services.AddTransient<IRestaurantService, RestaurantService>();
		builder.Services.AddTransient<IBatchService, BatchService>();
		builder.Services.AddTransient<IOrderService, OrderService>();
		builder.Services.AddTransient<IZoneService, ZoneService>();
		builder.Services.AddTransient<IInventoryItemService, InventoryItemService>();
		builder.Services.AddTransient<IUnitService, UnitService>();

		var selectedRestaurantTracker = new SelectedRestaurantTracker();
		builder.Services.AddSingleton(selectedRestaurantTracker);
		builder.Services.AddSingleton<ICurrentRestaurantService>(selectedRestaurantTracker);
        
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<LoginPage>();

        builder.Services.AddSingleton<LoadingViewModel>();
        builder.Services.AddSingleton<LoadingPage>();

		builder.Services.AddTransient<SampleDataService>();

		builder.Services.AddTransient<ItemsDetailViewModel>();
		builder.Services.AddTransient<ItemsDetailPage>();

        builder.Services.AddSingleton<ItemsViewModel>();
        builder.Services.AddSingleton<ItemsPage>();

		builder.Services.AddTransient<RestaurantsDetailViewModel>();
		builder.Services.AddTransient<RestaurantsDetailPage>();

        builder.Services.AddSingleton<RestaurantsViewModel>();
        builder.Services.AddSingleton<RestaurantsPage>();

		builder.Services.AddTransient<VendorsDetailViewModel>();
		builder.Services.AddTransient<VendorsDetailPage>();

        builder.Services.AddSingleton<VendorsViewModel>();
        builder.Services.AddSingleton<VendorsPage>();

        builder.Services.AddSingleton<BatchesViewModel>();
        builder.Services.AddSingleton<BatchesPage>();

        builder.Services.AddSingleton<BatchesDetailViewModel>();
        builder.Services.AddSingleton<BatchesDetailPage>();

        builder.Services.AddSingleton<OrdersDetailViewModel>();
        builder.Services.AddSingleton<OrdersDetailPage>();

		return builder.Build();
	}
}
