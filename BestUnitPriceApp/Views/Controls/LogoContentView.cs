using System;
namespace BestUnitPriceApp.Pages.Controls;

public class LogoContentView : ContentView
{

    public bool IsNavigation
    {
        get => (bool)GetValue(IsNavigationProperty);
        set => SetValue(IsNavigationProperty, value);
    }

    public static readonly BindableProperty IsNavigationProperty = BindableProperty.Create(
        nameof(IsNavigation), typeof(bool), typeof(LogoContentView));


    public LogoContentView()
    {
        var fontSize = IsNavigation ? 28 : 28; // 28 / 36
        var parent = new HorizontalStackLayout();
        var logo = new Label
        {
            FontFamily = "FontAwesome",
            FontSize = fontSize-2,
            Margin = new Thickness(0, 5, 0, 0),
            Text = Utilities.FontAwesomeHelper.TruckRampBox,
            VerticalTextAlignment = TextAlignment.Center
        };
        parent.Add(logo);
        Console.Write(fontSize);

        var best = new Label
        {
            Text = "Best",
            FontFamily = "BlackOpsOne",
            FontSize = fontSize,
            HorizontalTextAlignment = TextAlignment.Center,
            TextColor = Color.FromArgb("ffffff"),
            TextTransform = TextTransform.Uppercase,
            VerticalTextAlignment = TextAlignment.Center
        };
        parent.Add(best);

        var unit = new Label
        {
            Text = "Unit",
            FontFamily = "BlackOpsOne",
            FontSize = fontSize,
            HorizontalTextAlignment = TextAlignment.Center,
            TextColor = Color.FromArgb("574b43"),
            TextTransform = TextTransform.Uppercase,
            VerticalTextAlignment = TextAlignment.Center
        };
        parent.Add(unit);

        var price = new Label
        {
            Text = "price",
            FontFamily = "BlackOpsOne",
            FontSize = fontSize,
            HorizontalTextAlignment = TextAlignment.Center,
            TextColor = Color.FromArgb("6f665a"),
            TextTransform = TextTransform.Uppercase,
            VerticalTextAlignment = TextAlignment.Center
        };
        parent.Add(price);
        this.Content = parent;
    }
}
