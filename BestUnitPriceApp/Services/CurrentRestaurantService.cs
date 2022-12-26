namespace BestUnitPriceApp.Services;

public class CurrentRestaurantService: ICurrentRestaurantService
{
    private const string SelectedRestaurantKey = "SelectedRestaurant";

    private Restaurant _restaurant;

    public CurrentRestaurantService ()
    {
        var storedValue = Preferences.Get(SelectedRestaurantKey, null);
        if (!String.IsNullOrEmpty( storedValue))
        {
            try
            {
                _restaurant = System.Text.Json.JsonSerializer.Deserialize<Restaurant>(storedValue);
            }
            catch (Exception ex)
            {
                Preferences.Clear(SelectedRestaurantKey);
            }
        }
    }

    public Restaurant CurrentRestaurant { get { return _restaurant; }
        internal set
        {
            _restaurant = value;
            var json = System.Text.Json.JsonSerializer.Serialize(_restaurant);
            Preferences.Set(SelectedRestaurantKey, json);
            // store this into the 
        }
    } 
}

