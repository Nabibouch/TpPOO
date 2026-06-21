namespace tp_a_rendre.Domain;

public class Car
{
    public int Id {get; private set;}
    private string _name;
    private string _licensePlate;
    private int _year;
    private int _seatingCapacity;
    private decimal _price;
    private string _fuelType;
    private readonly List<Reservation> _reservations = new List<Reservation>();
    public IReadOnlyCollection<Reservation> Reservations => _reservations;
    private int _modelId;
    private Model _model;
    public int ModelId
    {
        get
        {
            return _modelId;
        }
    }
    public Model Model
    {
        get
        {
            return _model;
        }
        set
        {
            if (value is null)
            {
                throw new ArgumentException("Model cannot be null");
            }
            _model = value;
            _modelId = value.Id;
        }
    }
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Cannot be blank");
            }
            _name = value;
        }
    }
    public string LicensePlate
    {
        get
        {
            return _licensePlate;
        }
        set
        {
            if(value.Length != 9)
            {
                throw new ArgumentException("License need 9 characters"); //Juste pour avoir une règle métier (normalement en france c'est le cas)
            }
            _licensePlate = value;
        }
    }
    public int Year
    {
        get
        {
            return _year;
        }
        set
        {
            if(value > DateTime.Now.Year)
            {
                throw new ArgumentException("Cannot be in the future");
            }
            _year = value;
        }
    }
    public int SeatingCapacity
    {
        get
        {
            return _seatingCapacity;
        }
        set
        {
            if(value < 1) throw new ArgumentException("Must have atleast one seat");
            _seatingCapacity = value;
        }
    }
    public decimal Price
    {
        get
        {
            return _price;
        }
        set
        {
            if(value < 0) throw new ArgumentException("It's not charity");
            _price = value;
        }
    }
    public string FuelType
    {
        get
        {
            return _fuelType;
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Cannot be blank");
            }
            _fuelType = value;
        }
    }
    private Car() {}
    public Car(string name, string licensePlate, int year, int seatingCapacity, decimal price, string fuelType, Model model)
    {
        Name = name;
        LicensePlate = licensePlate;
        Year = year;
        SeatingCapacity = seatingCapacity;
        Price = price;
        FuelType = fuelType;
        Model = model;
    }

    public void AddReservation(Reservation reservation)
    {
        _reservations.Add(reservation);
    }
    public void DeleteReservation(Reservation reservation)
    {
        _reservations.Remove(reservation);
    }
}