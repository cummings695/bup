namespace BestUnitPriceApp.Views.Controls;

public partial class LogoView : HorizontalStackLayout
{

	public int Size
	{
		get => (int)GetValue(LogoProperty);
		set => SetValue(LogoProperty, value);
	}

	public static readonly BindableProperty LogoProperty = BindableProperty.Create(
		nameof(Size), typeof(int), typeof(LogoView));

	public LogoView()
	{
		InitializeComponent();
	}
}