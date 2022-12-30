namespace BestUnitPriceApp.Views.Controls;

public partial class AppTitleView : ContentView
{
	public AppTitleView()
	{
		InitializeComponent();
		BindingContext = new AppTitleViewModel();


	}
}