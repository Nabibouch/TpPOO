
public class Reservation
{
    public int Id {get; private set;}
    private DateTime _startOn;
    private DateTime _endOn;
    private int _carId;
    private Car _car;
    private int _clientId;
    private Client _client;
    public int CarId
    {
        get
        {
            return _carId;
        }
    }
    public Car Car
    {
        get
        {
            return _car;
        }
        set
        {
            if (value is null)
            {
                throw new ArgumentException("Car cannot be null");
            }
            _car = value;
            _carId = value.Id;
        }
    }
    public int ClientId
    {
        get
        {
            return _clientId;
        }
    }
    public Client Client
    {
        get
        {
            return _client;
        }
        set
        {
            if (value is null)
            {
                throw new ArgumentException("Client cannot be null");
            }
            _client = value;
            _clientId = value.Id;
        }
    }
    public DateTime StartOn
    {
        get
        {
            return _startOn;
        }
        set
        {
            if(value < DateTime.Now) throw new ArgumentException("Cannot take a reservation in the past");
            _startOn = value;
        }

    } 
    public DateTime EndOn
    {
        get
        {
            return _endOn;
        }
        set
        {
            if(value < DateTime.Now) throw new ArgumentException("Cannot end a reservation in the past");
            _endOn = value;
        }
    } 
    private Reservation() {}
    public Reservation(DateTime startOn, DateTime endOn, Car car, Client client)
    {
        StartOn = startOn;
        EndOn = endOn;
        Car = car;
        Client = client;
    }
}