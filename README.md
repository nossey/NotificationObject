# Description
This project tries to demonstrate notification object.

## NotificationObject
This object invokes PropertyChanged event.

```csharp
public class Weather : NotificationObject
{
	public int Temparature 
	{
		get{ return _Temparature;}
		set
		{
			SetValue(ref _Temparature, value);
		}
	}
	
	int _Temparature;
}

...

var weather = new Weather();
var listener = new PropertyChangedEventListener(meter, (sender, e)=>
{
	// ... do something
	if (e.PropertyName == nameof(Weather.Temparature))
	{
		...
	}
});

// listener recieves PropetyChanged event from meter.
weather.Temparature = 200;

// quit to listen weather's PropetyChanged
listener.Dispose();

```

## ReadonlySyncedCollection
ReadonlySyncedCollection just reflects source collection
Usually, and is created by "CreateReadonlySyncedCollection" method.

```csharp
public class Customer : NotificationObject
{
	public string Name
	{
		get { return _Name; }
		set { SetValue(ref _Name, value); } 
	}
	string _Name;
}

public class CustomerWatcher : IDisposable
{
	IDisposable listener;
	Customer Target;
	public void Dispose(Customer customer)
	{
		Target = customer;
		listener = new PropertyChangedEventListener(Target, (sender, e)=>{
			// ...do something
		});
	}

	public void Dispose()
	{
		listener?.dispose();
	}
}

...

var customers = new ObservableCollection<Customer>();
ReadonlySyncedCollection<CustomerWatcher> customerWatchers;
customerWatchers = CreateReadonlySyncedCollection(customers, 
(m)={return new CustomerWatcher(m);});

// customerWatchers automatically syncs with customers
customers.Add(new Customer());
customers.Clear();
```