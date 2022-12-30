using BestUnitPriceApp.Common.Messages;
using CommunityToolkit.Mvvm.Messaging;

namespace BestUnitPriceApp.Services;

public class CurrentRestaurantService: ICurrentRestaurantService
{
    private const string SelectedRestaurantKey = "SelectedRestaurant";

    public CurrentRestaurantService ()
    {
        var storedValue = Preferences.Get(SelectedRestaurantKey, null);
        if (!string.IsNullOrEmpty(storedValue))
        {
            try
            {
                CurrentRestaurant = System.Text.Json.JsonSerializer.Deserialize<Restaurant>(storedValue);
            }
            catch (Exception ex)
            {
                Preferences.Clear(SelectedRestaurantKey);
            }
        }

        WeakReferenceMessenger.Default.Register<SelectedRestaurantChangedMessage>(this, (r, m) =>
        {
            // Handle the message here, with r being the recipient and m being the
            // input message. Using the recipient passed as input makes it so that
            // the lambda expression doesn't capture "this", improving performance.
            CurrentRestaurant = m.Value;
            var json = System.Text.Json.JsonSerializer.Serialize(CurrentRestaurant);
            Preferences.Set(SelectedRestaurantKey, json);
        });
    }

    public Restaurant CurrentRestaurant { get; private set; }
}

