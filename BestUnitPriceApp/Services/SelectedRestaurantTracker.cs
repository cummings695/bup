namespace BestUnitPriceApp.Services;

public class SelectedRestaurantTracker : IObservable<Restaurant>, ICurrentRestaurantService
{
    private class Unsubscriber : IDisposable
    {
        private List<IObserver<Restaurant>>_observers;
        private IObserver<Restaurant> _observer;

        public Unsubscriber(List<IObserver<Restaurant>> observers, IObserver<Restaurant> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }

    private Restaurant _selectedRestaurant = null;
    private List<IObserver<Restaurant>> _observers;
    
    public SelectedRestaurantTracker()
    {
        _observers = new List<IObserver<Restaurant>>();
    }
    
    public IDisposable Subscribe(IObserver<Restaurant> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
            //if (_selectedRestaurant != null)
            //    observer.OnNext(_selectedRestaurant);
        }

        return new Unsubscriber(_observers, observer);
    }

    public void TrackRestaurant(Restaurant restaurant)
    {
        this._selectedRestaurant = restaurant;

        foreach (var observer in _observers)
        {
            if (restaurant == null)
            {
                observer.OnError(new ApplicationException());
                continue;
            }

            observer.OnNext(restaurant);
        }
    }

    public void EndTransmission()
    {
        foreach (var observer in _observers.ToArray())
        {
            if (_observers.Contains(observer))
            {
                observer.OnCompleted();
            }
        }
        _observers.Clear();
    }

    public Restaurant CurrentRestaurant
    {
        get
        {
            return _selectedRestaurant;
        }
    }
}


public class RestaurantReporter : IObserver<Restaurant>
{
    private IDisposable unsubscriber;
    private string instName;

    public RestaurantReporter(string name)
    {
        this.instName = name;
    }

    public string Name
    {  get{ return this.instName; } }

    public virtual void Subscribe(IObservable<Restaurant> provider)
    {
        if (provider != null)
            unsubscriber = provider.Subscribe(this);
    }

    public virtual void OnCompleted()
    {
        Console.WriteLine("The Location Tracker has completed transmitting data to {0}.", this.Name);
        this.Unsubscribe();
    }

    public virtual void OnError(Exception e)
    {
        Console.WriteLine("{0}: The location cannot be determined.", this.Name);
    }

    public virtual void OnNext(Restaurant value)
    {
        Console.WriteLine("The current restaurant is {0}", value.Name);
    }

    public virtual void Unsubscribe()
    {
        unsubscriber.Dispose();
    }
}