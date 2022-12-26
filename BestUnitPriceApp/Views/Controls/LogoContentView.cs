using System;
namespace BestUnitPriceApp.Pages.Controls;

public class LogoContentView : ContentView
{
    public static readonly BindableProperty IsNavigationProperty = BindableProperty.Create(nameof(IsNavigation), typeof(bool), typeof(LogoContentView), default(bool));

    public LogoContentView()
    {
        var parent = new HorizontalStackLayout();
        var logo = new Label
        {
            FontFamily = "FontAwesome",
            FontSize = IsNavigation ? 24 : 32,
            Text = Utilities.FontAwesomeHelper.TruckRampBox,
            VerticalTextAlignment = TextAlignment.Center
        };
        parent.Add(logo);
        var fontSize = IsNavigation ? 24 : 36;
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

    public bool IsNavigation
    {
        get => (bool)GetValue(IsNavigationProperty);
        set => SetValue(IsNavigationProperty, value);
    }
}

